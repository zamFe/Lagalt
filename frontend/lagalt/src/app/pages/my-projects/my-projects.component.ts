import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html',
  styleUrls: ['./my-projects.component.css']
})
export class MyProjectsComponent implements OnInit{

  constructor(private readonly projectService: ProjectService, private readonly skillsService: SkillService) {  }

  ngOnInit(): void {
    this.projectService.getProjects();
    this.skillsService.getSkills();

  }


}
