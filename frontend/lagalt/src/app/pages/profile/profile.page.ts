import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { NgForm } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';
import { UserService } from 'src/app/services/user.service';
import { SkillService } from 'src/app/services/skill.service';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { Skill } from 'src/app/models/skill.model';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.css'],
})
export class ProfilePage implements OnInit, OnDestroy {
  public profileJson: string = '';
  private user$: Subscription;
  private skills$: Subscription;
  public user: UserComplete = {
    id: 0,
    username: '',
    description: '',
    image: '',
    portfolio: '',
    skills: [],
    projects: [],
    hidden: false,
  };

  public skill: Skill = {
    id: 0,
    name: '',
  };

  public isChecked = true;
  public color = 'accent';

  constructor(
    private readonly userService: UserService,
    public auth: AuthService,
    private readonly skillService: SkillService,
    private readonly projectService: ProjectService
  ) {
    this.user$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user = user;
      localStorage.setItem('userId', JSON.stringify(user.id));
    });

    this.skills$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user.skills = user.skills;
    });
  }

  ngOnInit(): void {
    this.auth.idTokenClaims$.subscribe((data) =>
      localStorage.setItem('token', data!.__raw)
    );
    this.auth.user$.subscribe((profile) => {
      this.user.username = JSON.parse(
        JSON.stringify(profile?.nickname, null, 2)
      );
      this.userService
        .userExists(this.user.username)
        .pipe(finalize(() => {}))
        .subscribe(
          (res) => {
            if (res) {
              this.userService.getUserByUsername(this.user.username);
            }
          },
          () => {
            this.userService.postUserByUsername(this.user.username);
          }
        );
    });
    this.skillService.getSkills();
  }

  ngOnDestroy(): void {
    this.user$.unsubscribe();
  }

  //Changes the hidden mode button from true to false or false to true
  changed() {}

  //Adds skill to a list in the user profile
  addSkill(addSkillForm: NgForm) {
    let userId: number[] = [];
    this.userService.user$.subscribe((user: UserComplete) => {
      userId = [user.id];
    });

    let newSkill: Object = {
      name: addSkillForm.value.skills,
      users: userId,
      projects: [],
    };

    let skillslist: Skill[] = [];
    this.skillService.skills$.subscribe((data) => (skillslist = data));
    let tempSkill = null;
    tempSkill = skillslist.find(
      (element) => element.name === addSkillForm.value.skills
    );
    if (tempSkill !== undefined) {
      this.user.skills.push(tempSkill);
      this.userService.putUser();
    } else {
      this.skillService.postSkill(newSkill);
    }
  }

  refresh() {
    setTimeout(function () {
      window.location.reload();
    }, 1000);
  }

  handleDescription(handleDescriptionForm: NgForm) {
    this.user.description = handleDescriptionForm.value.description;
    this.userService.putUser();
  }

  get loading(): boolean {
    return this.projectService.loading;
  }
}
