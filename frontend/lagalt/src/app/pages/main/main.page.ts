import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { SkillService } from 'src/app/services/skill.service';
import { Router, RouterLink, NavigationExtras } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css']
})
export class MainPage implements OnInit {

  constructor(private readonly projectService: ProjectService,
     private readonly skillsService: SkillService, private readonly router : Router) { }

  ngOnInit(): void {
    // if (this.projectService.renderProjects$.value.length !== 0) {
    //   this.projectService.getProjectsAndSetRender();
    // }
    // else {
      
    // }
    this.projectService.getProjects();
    this.skillsService.getSkills();
    
  }
  goToProject(){
    this.router.navigate(["project"])
  }


}
