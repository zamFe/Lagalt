import { Component, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApplicationService } from 'src/app/services/application.service';
import { Application, ApplicationResponse, PutApplication } from 'src/app/models/application.model';
import { Skill } from 'src/app/models/skill.model';
import { ActivatedRoute } from '@angular/router';
import { SkillService } from 'src/app/services/skill.service';
import { ProjectService } from 'src/app/services/project.service';
import { UserService } from 'src/app/services/user.service';
import { inject } from '@angular/core/testing';
import { Project } from 'src/app/models/project.model';
import { UserComplete } from 'src/app/models/user/user-complete.model';

@Component({
  selector: 'app-admin-give-access',
  templateUrl: './admin-give-access.page.html',
  styleUrls: ['./admin-give-access.page.css']
})
export class AdminGiveAccessPage implements OnInit {

  private skillsNeeded$: Subscription;
  private application$: Subscription
  private user$: Subscription;
  private readonly projectId: number = 0
  private putApplication : PutApplication = {
    id: 0,
    accepted: false,
    seen: false
  }

  public skillsNeeded: Skill[] = []
  public applications: Application[] = []
  public skillMatched : String = ""
  public project : Project = {
    id: 0,
    profession: {id: 0, name: ""},
    title: '',
    image: '',
    skills: [],
    users: [],
    description: '',
    progress: '',
    source: "",
    administratorIds: []
  }
  public user: UserComplete = {
    id: 0,
    username: '',
    description: '',
    image: '',
    portfolio: '',
    skills: [],
    projects: [],
    hidden: false
  }

  constructor(private readonly applicationService : ApplicationService,
    private route : ActivatedRoute,
    private readonly skillsService : SkillService,
    private readonly projectService : ProjectService,
    private readonly userService : UserService) {

    this.projectId = Number(this.route.snapshot.params.id)

    this.application$ = this.applicationService.applications$.subscribe((application : Application[]) => {
      this.applications = application

    })
    this.skillsNeeded$ = this.skillsService.skills$.subscribe((skills: Skill[]) => {
      this.skillsNeeded = skills.filter(s => this.project.skills.includes(s))
    })
    this.user$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user = user;
    })

    this.projectService.project$.subscribe((project : Project) => {
      this.project = project
    })

    // console.log(this.applications);


  }

  declineApplication(id: number){
    this.putApplication = {
      id : id,
      accepted : false,
      seen : true
    }
    this.applicationService.putApplication(this.putApplication)
    this.applications = this.applications.filter(element => element.id !== id)
  }

  acceptApplication(id: number){
    this.putApplication = {
      id : id,
      accepted : true,
      seen : true
    }
    this.applicationService.putApplication(this.putApplication)
    this.applications = this.applications.filter(element => element.id !== id)
  }

  ngOnInit(): void {

    this.applicationService.getApplicationsByProjectId(this.projectId)
  }
  matchSkills() {
    for (let i = 0; i < this.project.skills.length; i++) {
      if(this.user.skills.find(s => s.name === this.project.skills[i].name)){
        this.skillMatched = "Matched"
        break
      }
    }
  }

  get loading(): boolean {
    return this.projectService.loading;
  }

}
