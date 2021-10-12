import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { Project } from 'src/app/models/project.model';



@Component({
  selector: 'app-project',
  templateUrl: './project.page.html',
  styleUrls: ['./project.page.css']
})
export class ProjectPage implements OnInit {

  // projectIndusty: string = 'Creative field(film)'
  // projectName: string = "Project name"
  // userName: string = 'Michael Jordan'
  // date: string = "01.01.2012"
  // projectStatus: string = "Under dev"
  // skillsNeeded: string[] = ['web dev', 'C#', 'Angular']
  // projectImage: string = "https://avatars.dicebear.com/api/initials/cm.svg"
  // projecDescription: string = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
  // projectTags: string[] = ['#test', '#lagalt']

  constructor(private readonly projectService: ProjectService) { 
  }

  ngOnInit(): void {
    this.projectService.getProjectById(5)
  }

  get project$(): Observable<Project> {
    return this.projectService.getProject$();
  }

}
