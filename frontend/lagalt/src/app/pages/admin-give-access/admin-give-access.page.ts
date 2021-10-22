import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApplicationService } from 'src/app/services/application.service';
import { Application, ApplicationResponse, PutApplication } from 'src/app/models/application.model';
import { Skill } from 'src/app/models/skill.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-admin-give-access',
  templateUrl: './admin-give-access.page.html',
  styleUrls: ['./admin-give-access.page.css']
})
export class AdminGiveAccessPage implements OnInit {

  private application$: Subscription
  private readonly projectId: number = 0
  public applications: Application[] = []
  private putApplication : PutApplication = {
    id: 0,
    accepted: false,
    seen: false
  }


  constructor(private readonly applicationService : ApplicationService, private route : ActivatedRoute) {
    this.projectId = Number(this.route.snapshot.params.id)

    this.application$ = this.applicationService.applications$.subscribe((application : Application[]) => {
      this.applications = application

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
  }

  acceptApplication(id: number){
    this.putApplication = {
      id : id,
      accepted : true,
      seen : true
    }
    this.applicationService.putApplication(this.putApplication)
  }

  ngOnInit(): void {

    this.applicationService.getApplicationsByProjectId(this.projectId)
  }

}
