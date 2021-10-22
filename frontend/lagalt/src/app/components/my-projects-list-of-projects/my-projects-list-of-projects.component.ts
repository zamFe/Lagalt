import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink, NavigationExtras, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { MyProjectService } from 'src/app/services/my-project.service';
@Component({
  selector: 'app-my-projects-list-of-projects',
  templateUrl: './my-projects-list-of-projects.component.html',
  styleUrls: ['./my-projects-list-of-projects.component.css']
})
export class MyProjectsListOfProjectsComponent implements OnInit, OnDestroy {

  private projects$: Subscription;
  public projects: Project[] = [];

  constructor(private readonly myProjectService: MyProjectService, private readonly router : Router) {
    this.projects$ = this.myProjectService.myProjects$.subscribe((projects: Project[]) => {
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
