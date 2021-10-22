import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { Profession } from 'src/app/models/profession.model';
import { MessageService } from 'src/app/services/message.service';
import { Message } from 'src/app/models/message.model';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute } from '@angular/router';



@Component({
  selector: 'app-project',
  templateUrl: './project.page.html',
  styleUrls: ['./project.page.css']
})
export class ProjectPage implements OnInit, OnDestroy {

  private readonly projectId: number = 0;
  private project$: Subscription

  public project: Project = {
    id: 0,
    profession: {id : 0, name : "Music"},
    title: '',
    image: '',
    skills: [],
    users: [],
    description: '',
    progress: '',
    source: null,
    administratorIds: []
  };
  private userIdsInProject: number[] = [];
  private userId: number = 0;
  constructor(private readonly projectService: ProjectService,
              private readonly messageService : MessageService,
              private readonly userService: UserService,
              private route: ActivatedRoute) {

    this.projectId = Number(this.route.snapshot.params.id)
    this.project$ = this.projectService.project$.subscribe((project: Project) => {
      this.project = project;
      this.userIdsInProject = project.users.map(u => {
        return u.id
      })
    })
    this.userService.user$.subscribe(user => this.userId = user.id)
  }

  ngOnInit(): void {
    this.projectService.getProjectById(this.projectId)
    
    // CHECK IF USER IS IN PROJECT
    if (this.userIdsInProject.includes(this.userId)) {
      this.messageService.getMessagesByProjectId(this.projectId)
    }
  }

  ngOnDestroy(): void {
    this.project$.unsubscribe();
  }

}
