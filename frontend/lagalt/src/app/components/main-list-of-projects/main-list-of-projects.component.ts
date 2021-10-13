import { Component, OnDestroy, OnInit } from '@angular/core';
import { Project } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-main-list-of-projects',
  templateUrl: './main-list-of-projects.component.html',
  styleUrls: ['./main-list-of-projects.component.css']
})
export class MainListOfProjectsComponent implements OnInit, OnDestroy {

  private projects$: Subscription;
  public projects: Project[] = [];
  constructor(private readonly projectService: ProjectService) {
    this.projects$ = this.projectService.projects$.subscribe((projects: Project[]) => {
      this.projects = projects
    })
  }


  ngOnInit(): void {
  }
  
  ngOnDestroy(): void {
    this.projects$.unsubscribe();
  }

}
