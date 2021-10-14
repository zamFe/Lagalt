import { Component, OnDestroy, OnInit } from '@angular/core';
import { Project } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-project-status',
  templateUrl: './project-status.component.html',
  styleUrls: ['./project-status.component.css']
})

export class ProjectStatusComponent implements OnInit, OnDestroy {


  private progress$: Subscription;
  public progress: string = "";
  public status : string[] = ['Founding', 'In progress', 'Stalled', 'Completed'];

  constructor(private readonly projectService: ProjectService) {
    this.progress$ = this.projectService.project$.subscribe((project: Project) => {
      this.progress = project.progress
      console.log(this.progress);

    })
  }


  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.progress$.unsubscribe();
  }

}
