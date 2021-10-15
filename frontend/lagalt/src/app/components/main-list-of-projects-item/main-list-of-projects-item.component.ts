import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { SkillService } from 'src/app/services/skill.service';
import { Skill } from 'src/app/models/skill.model'
import { Project } from 'src/app/models/project.model';

@Component({
  selector: 'app-main-list-of-projects-item',
  templateUrl: './main-list-of-projects-item.component.html',
  styleUrls: ['./main-list-of-projects-item.component.css']
})
export class MainListOfProjectsItemComponent implements OnInit, OnDestroy {
  @Input() project: Project = {
    id: 0,
    profession: {id : 0, name : ""},
    title: '',
    image: '',
    skills: [],
    users: [],
    description: '',
    progress: '',
    source: null,
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
