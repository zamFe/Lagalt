import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { Profession } from 'src/app/models/profession.model';
import { MessageService } from 'src/app/services/message.service';
import { Message } from 'src/app/models/message.model';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute } from '@angular/router';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { NgForm } from '@angular/forms';
import { SkillService } from 'src/app/services/skill.service';




@Component({
  selector: 'app-project',
  templateUrl: './project.page.html',
  styleUrls: ['./project.page.css']
})
export class ProjectPage implements OnInit, OnDestroy {
  private readonly projectId: number = 0
  private project$: Subscription;
  private user$: Subscription;

  public project: Project = {
    id: 0,
    profession: {id : 0, name : "Music"},
    title: '',
    image: '',
    skills: [],
    users: [],
    description: '',
    progress: '',
    source: '',
    administratorIds: []
  };

  public user: UserComplete = {
    id: 0,
    username: '',
    description: '',
    image: '',
    portfolio: '',
    skills: [],
    projects: [],
    hidden: false,
  };

  public userId : number = 0;
  public adminId : number[] = [];
  public userRole: string = "";
  public adminName : string = "";
  private userIdsInProject: number[] = [];
  constructor(private readonly projectService : ProjectService,
              private readonly messageService : MessageService,
              private readonly userService : UserService,
              private readonly skillService : SkillService,
              private route: ActivatedRoute) {

    this.projectId = Number(this.route.snapshot.params.id)
    this.project$ = this.projectService.project$.subscribe((project: Project) => {
      this.project = project;
      this.userIdsInProject = project.users.map(u => {
        return u.id
      })
    })

    this.project$ = this.projectService.project$.subscribe((project : Project) => {
      this.adminId = project.administratorIds

    })

    this.user$ = this.userService.user$.subscribe((user : UserComplete) => {
      this.userId = user.id
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


  addSkill(addSkillForm : NgForm){

    let projectId : number[] = []
    this.projectService.project$.subscribe((project : Project) => {
      projectId = [project.id]
    })

    let newSkill : Object = {
      name: addSkillForm.value.skills,
      users: [],
      projects: projectId
    }
    this.skillService.postSkill(newSkill)
  }

  refresh() {
    setTimeout(function(){
      window.location.reload();
        },1000)
  }

  get loading(): boolean {
    return this.projectService.loading;
  }


}
