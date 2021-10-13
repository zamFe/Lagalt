import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css']
})
export class MainPage implements OnInit {

  constructor(private readonly projectService: ProjectService, private readonly skillsService: SkillService) { }

  ngOnInit(): void {
    this.projectService.getProjects();
    this.skillsService.getSkills();
  }

}
