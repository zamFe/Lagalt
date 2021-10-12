import { Component, OnInit } from '@angular/core';
import { Project } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-main-list-of-projects',
  templateUrl: './main-list-of-projects.component.html',
  styleUrls: ['./main-list-of-projects.component.css']
})
export class MainListOfProjectsComponent implements OnInit {

  constructor(private readonly projectService: ProjectService) { }

  ngOnInit(): void {
  }

  get projects$(): Observable<Project[]> {
    return this.projectService.getRenderProjects$();
  }

}
