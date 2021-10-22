import { Component, OnDestroy, OnInit } from '@angular/core';
import { Project, PutProject } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { MatRadioChange } from '@angular/material/radio';

@Component({
  selector: 'app-project-status',
  templateUrl: './project-status.component.html',
  styleUrls: ['./project-status.component.css']
})

export class ProjectStatusComponent implements OnInit, OnDestroy {


  private progress$: Subscription;
  private project$: Subscription
  private user$ : Subscription
  public progress: string = "";
  public status : string[] = ['Founding', 'In progress', 'Stalled', 'Completed'];
  public administratorIds : number[] = [];
  public userId : number = 0;
  public userRole: string = ""
  public putProject: PutProject = {
    id: 0,
    skills: [],
    title: '',
    description: '',
    progress: '',
    image: '',
    source: '',
  }


  constructor(private readonly projectService: ProjectService, private readonly userService: UserService) {

    this.progress$ = this.projectService.project$.subscribe((project: Project) => {
      this.progress = project.progress

    })
    this.project$ = this.projectService.project$.subscribe((project : Project) => {
      this.administratorIds = project.administratorIds
      this.putProject.id = project.id
      this.putProject.description = project.description
      this.putProject.image = project.image
      this.putProject.skills = project.skills
      this.putProject.source = project.source
      this.putProject.title = project.title

    })
    this.user$ = this.userService.user$.subscribe((user : UserComplete) => {
      this.userId = user.id

    })
    if (this.userId !== 0 && this.administratorIds.includes(this.userId)) {
      this.userRole = "admin"
    }
    else if (this.userId !== 0) {
      this.userRole = "user"

    }
    else {
      this.userRole = "guest"

    }
  }

  ifAdmin(){
    // Check if current user is in the projects admin id list

  }
  radioChange($event : MatRadioChange){
    this.putProject.progress = $event.source.value

    this.projectService.putProject(this.putProject)

  }


  ngOnInit(): void {


  }
  ngOnDestroy(): void {
    this.progress$.unsubscribe();
  }

}
