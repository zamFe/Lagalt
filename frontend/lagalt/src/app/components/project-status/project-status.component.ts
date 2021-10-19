import { Component, OnDestroy, OnInit } from '@angular/core';
import { Project } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { UserComplete } from 'src/app/models/user/user-complete.model';

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
  public adminId : number[] | undefined;
  public userId : number | undefined;
  public userRole: string = ""
  constructor(private readonly projectService: ProjectService, private readonly userService: UserService) {
    this.progress$ = this.projectService.project$.subscribe((project: Project) => {
      this.progress = project.progress
      console.log(this.progress);

    })
    this.project$ = this.projectService.project$.subscribe((project : Project) => {
      this.adminId = project.administratorIds
      console.log(this.adminId);
      
      
    })
    this.user$ = this.userService.user$.subscribe((user : UserComplete) => {
      this.userId = user.id
      console.log(this.userId);
      
    })
    if (this.userId && this.adminId?.includes(this.userId)) {
      this.userRole = "admin"
    }
    else if (this.userId) {
      this.userRole = "user"
    }
    else {
      this.userRole = "guest"
    }
  }

  ifAdmin(){
    // Check if current user is in the projects admin id list
    
  }


  ngOnInit(): void {  
    
    
  }
  ngOnDestroy(): void {
    this.progress$.unsubscribe();
  }

}
