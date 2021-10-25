import { Component, OnDestroy, OnInit } from '@angular/core';
import { PutUser, UserComplete } from 'src/app/models/user/user-complete.model';
import { NgForm, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';
import { UserService } from 'src/app/services/user.service';
import { SkillService } from 'src/app/services/skill.service';
import { Observable, Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { Skill, IdSkill } from 'src/app/models/skill.model';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.css']
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
    hidden: false
  }
  public putUser: PutUser = {
    id: 0,
    skills: [],
    hidden: false,
    username: '',
    description: '',
    image: '',
    portfolio: ''
  }

  public skill: Skill = {
    id: 0,
    name: ''
  }

  public isChecked = true;
  public color = 'accent';

  constructor(private readonly userService : UserService, public auth: AuthService,
    private readonly skillService : SkillService, private readonly projectService: ProjectService) {
    this.user$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user = user;
      localStorage.setItem('userId', JSON.stringify(user.id));
    })

    this.skills$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user.skills = user.skills
    })

  }

  ngOnInit(): void {
    this.auth.idTokenClaims$.subscribe(data => localStorage.setItem('token', data!.__raw));
    this.auth.user$.subscribe(
      (profile) => {
        this.user.username = JSON.parse(JSON.stringify(profile?.nickname, null, 2))
        this.userService.userExists(this.user.username).pipe(finalize(() => {
        })).subscribe(res => {
          if(res){
            this.userService.getUserByUsername(this.user.username)
         }
        }, () => {
          this.userService.postUserByUsername(this.user.username)
        });

      }
    );
  }


  ngOnDestroy(): void {
    this.user$.unsubscribe();
  }

  //Changes the hidden mode button from true to false or false to true
  changed(){
  }


  //Adds skill to a list in the user profile
  addSkill(addSkillForm : NgForm){

    let userId : number[] = []
    this.userService.user$.subscribe((user : UserComplete) => {
      userId = [user.id]
    })

    let newSkill : Object = {
      name: addSkillForm.value.skills,
      users: userId,
      projects: []
    }


    this.skillService.postSkill(newSkill)
  }



  refresh() {
    setTimeout(function(){
      window.location.reload();
        },1000)
  }

  handleDescription(handleDescriptionForm : NgForm){
    this.putUser = {
      username : this.user.username,
      id: this.user.id,
      description: handleDescriptionForm.value.description,
      image: this.user.image,
      portfolio: this.user.portfolio,
      skills: this.user.skills,
      hidden: this.user.hidden,
      }
    console.log(this.putUser);

      this.userService.putUser(this.putUser);
  }
  saveUser() {
    //this.userService.putUser(this.user)
  }

}
