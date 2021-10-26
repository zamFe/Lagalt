import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { Skill } from 'src/app/models/skill.model';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-my-projects-list-of-projects-item',
  templateUrl: './my-projects-list-of-projects-item.component.html',
  styleUrls: ['./my-projects-list-of-projects-item.component.css']
})
export class MyProjectsListOfProjectsItemComponent implements OnInit, OnDestroy {

  @Input() project: Project = {
    id: 0,
    profession: {id : 0, name : ""},
    title: '',
    image: '',
    skills: [],
    users: [],
    description: '',
    progress: '',
    source: '',
    administratorIds: []
  };

  private skillsNeeded$: Subscription;
  public skillsNeeded: Skill[] = []

  constructor(private readonly skillsService: SkillService) {
    this.skillsNeeded$ = this.skillsService.skills$.subscribe((skills: Skill[]) => {
      this.skillsNeeded = skills.filter(s => this.project.skills.includes(s))
    })
   }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.skillsNeeded$.unsubscribe();
  }

}
