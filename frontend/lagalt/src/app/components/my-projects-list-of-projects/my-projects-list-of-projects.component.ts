import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-my-projects-list-of-projects',
  templateUrl: './my-projects-list-of-projects.component.html',
  styleUrls: ['./my-projects-list-of-projects.component.css']
})
export class MyProjectsListOfProjectsComponent implements OnInit, OnDestroy {

  private projects$: Subscription;
  public projects: Project[] = [];

  constructor(private readonly projectService: ProjectService) {
    this.projects$ = this.projectService.renderProjects$.subscribe((projects: Project[]) => {
      this.projects = projects
    })
   }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.projects$.unsubscribe();
  }

}
