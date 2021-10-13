import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SkillService } from 'src/app/services/skill.service';
import { Skill } from 'src/app/models/skill.model'
import { Project } from 'src/app/models/project.model';

@Component({
  selector: 'app-main-list-of-projects-item',
  templateUrl: './main-list-of-projects-item.component.html',
  styleUrls: ['./main-list-of-projects-item.component.css']
})
export class MainListOfProjectsItemComponent implements OnInit {
  @Input() project: Project = {
    id: 0,
    profession: 0,
    title: '',
    image: '',
    skills: [],
    messages: [],
    users: [],
    description: '',
    progress: '',
    source: '',
  };

  public skills: Skill[] = []
  constructor(private readonly skillsService: SkillService) { 

  }

  ngOnInit(): void {
    this.skills$.subscribe(data => {
      this.skills = data
    })
  }



  get skills$(): Observable<Skill[]> {
    return this.skillsService.getSkills$();
  }


}
