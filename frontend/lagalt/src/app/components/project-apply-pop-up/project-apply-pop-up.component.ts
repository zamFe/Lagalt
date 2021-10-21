import {Component, OnDestroy, OnInit} from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Project } from "../../models/project.model";
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { UserComplete } from 'src/app/models/user/user-complete.model';

@Component({
  selector: 'app-project-apply-pop-up',
  templateUrl: './project-apply-pop-up.component.html',
  styleUrls: ['./project-apply-pop-up.component.css']
})
export class ProjectApplyPopUpComponent implements OnInit, OnDestroy {
  private project$: Subscription
  private user$ : Subscription
  public progress: string = "";
  public adminId : number[] | undefined;
  public userId : number | undefined;
  public userRole: string = ""

  closeResult = '';

  projectName: string = "Project name"
  public textAreaForm: FormGroup;

  constructor(private modalService: NgbModal, private fb : FormBuilder,
    private readonly projectService: ProjectService, private readonly userService: UserService) {
    this.textAreaForm = fb.group({
      textArea: ""
    });


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
