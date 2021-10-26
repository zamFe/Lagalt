import { Component, OnInit } from '@angular/core';

import { MyProjectService } from 'src/app/services/my-project.service';
import { SkillService } from 'src/app/services/skill.service';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html',
  styleUrls: ['./my-projects.component.css']
})
export class MyProjectsComponent implements OnInit{

  public userId: number = 0;
  constructor(private readonly myProjectService: MyProjectService,
    private readonly skillsService: SkillService) {
  }
  


  get totalPages(): number {
    return this.myProjectService.pages;
  }
  get currentPage(): number {
    return this.myProjectService.currentPage;
  }

  onNextClick() {
    this.myProjectService.nextPage(this.userId);
  }
  
  onPrevClick() {
    this.myProjectService.prevPage(this.userId);
  }

  ngOnInit(): void {
    this.userId = Number(localStorage.getItem('userId'))
    this.myProjectService.getProjectsByUserId(this.userId);
    this.skillsService.getSkills();
    
  }


}
