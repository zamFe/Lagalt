import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { SkillService } from 'src/app/services/skill.service';
import { Skill } from 'src/app/models/skill.model';
import { Project } from 'src/app/models/project.model';
import { UserService } from 'src/app/services/user.service';
import { UserComplete } from 'src/app/models/user/user-complete.model';

@Component({
  selector: 'app-main-list-of-projects-item',
  templateUrl: './main-list-of-projects-item.component.html',
  styleUrls: ['./main-list-of-projects-item.component.css'],
})
export class MainListOfProjectsItemComponent implements OnInit, OnDestroy {
  @Input() project: Project = {
    id: 0,
    profession: { id: 0, name: '' },
    title: '',
    image: '',
    skills: [],
    users: [],
    description: '',
    progress: '',
    source: '',
    administratorIds: [],
  };

  private skillsNeeded$: Subscription;
  public skillsNeeded: Skill[] = [];

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
  private user$: Subscription;

  public skillMatched: String = '';

  constructor(
    private readonly skillsService: SkillService,
    private readonly userService: UserService
  ) {
    this.skillsNeeded$ = this.skillsService.skills$.subscribe(
      (skills: Skill[]) => {
        this.skillsNeeded = skills.filter((s) =>
          this.project.skills.includes(s)
        );
      }
    );

    this.user$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user = user;
    });
  }

  ngOnInit(): void {
    this.matchSkills();
  }

  ngOnDestroy(): void {
    this.skillsNeeded$.unsubscribe();
  }

  matchSkills() {
    for (let i = 0; i < this.project.skills.length; i++) {
      if (
        this.user.skills.find((s) => s.name === this.project.skills[i].name)
      ) {
        this.skillMatched = 'Matched';
        break;
      }
    }
  }
}
