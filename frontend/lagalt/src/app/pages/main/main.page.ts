import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { SkillsService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css']
})
export class MainPage implements OnInit {

  constructor(private readonly projectService: ProjectService, private readonly skillsService: SkillsService) { }

  ngOnInit(): void {
    this.projectService.getProjects();
    this.skillsService.getSkills();
  }

}
