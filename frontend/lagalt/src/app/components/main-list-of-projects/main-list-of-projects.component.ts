import { Component, OnDestroy, OnInit } from '@angular/core';
import { Project } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-list-of-projects',
  templateUrl: './main-list-of-projects.component.html',
  styleUrls: ['./main-list-of-projects.component.css']
})
export class MainListOfProjectsComponent implements OnInit, OnDestroy {

  private projects$: Subscription;
  public projects: Project[] = [];
  
  constructor(private readonly projectService: ProjectService, private router: Router) {
    this.projects$ = this.projectService.projects$.subscribe((projects: Project[]) => {
      this.projects = projects
    })
  }

  goToProject(id: number){
    this.router.navigateByUrl(`/project/${id}`)
  }

  ngOnInit(): void {
  }
  
  ngOnDestroy(): void {
    this.projects$.unsubscribe();
  }

}
