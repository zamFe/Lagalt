import {Component, OnDestroy, OnInit} from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Project } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { Application, PostApplication } from 'src/app/models/application.model';
import { ApplicationService } from 'src/app/services/application.service';

@Component({
  selector: 'app-project-apply-pop-up',
  templateUrl: './project-apply-pop-up.component.html',
  styleUrls: ['./project-apply-pop-up.component.css']
})
export class ProjectApplyPopUpComponent implements OnInit, OnDestroy {
  private project$: Subscription
  private user$ : Subscription
  public progress: string = "";
  public adminId : number[] = [];
  public userId : number = 0;
  public projectId : number = 0
  public userRole: string = ""
  public userAccept : boolean = true

  public application : PostApplication = {
    projectId: 0,
    userId: 0,
    motivation: ''
  }

  closeResult = '';

  projectName: string = "Project name"
  public textAreaForm: FormGroup;

  constructor(private modalService: NgbModal, private fb : FormBuilder,
    private readonly projectService: ProjectService, private readonly userService: UserService,
    private readonly applicationService : ApplicationService) {
    this.textAreaForm = fb.group({
      textArea: ""
    });

    this.project$ = this.projectService.project$.subscribe((project : Project) => {
      this.projectId = project.id
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
  applyToProject(){
    this.application.userId = this.userId
    this.application.projectId = this.projectId
    this.application.motivation = this.textAreaForm.value.textArea
    this.applicationService.postApplication(this.application)
  }

  open(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'});
  }

  ifAdmin(){
    // Check if current user is in the projects admin id list

  }


  ngOnInit(): void {


  }
  ngOnDestroy(): void {
  }

}
