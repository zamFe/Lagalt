import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Project } from '../../models/project.model';
import { ProjectService } from 'src/app/services/project.service';
import { Observable, Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-applications',
  templateUrl: './project-applications.component.html',
  styleUrls: ['./project-applications.component.css'],
})
export class ProjectApplicationsComponent implements OnInit, OnDestroy {
  private project$: Subscription;
  private user$: Subscription;
  public progress: string = '';
  public adminId: number[] = [];
  public userId: number = 0;
  public userRole: string = '';
  public projectId: number = 0;

  constructor(
    private modalService: NgbModal,
    private readonly projectService: ProjectService,
    private readonly userService: UserService,
    private readonly router: Router
  ) {
    this.project$ = this.projectService.project$.subscribe(
      (project: Project) => {
        this.projectId = project.id;
      }
    );

    this.project$ = this.projectService.project$.subscribe(
      (project: Project) => {
        this.adminId = project.administratorIds;
      }
    );

    this.user$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.userId = user.id;
    });

    if (this.userId && this.adminId?.includes(this.userId)) {
      this.userRole = 'admin';
    } else if (this.userId) {
      this.userRole = 'user';
    } else {
      this.userRole = 'guest';
    }
  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  goToApplication(id: number) {
    this.router.navigate([`project/${id}/applications`]);
  }
  ngOnInit(): void {}

  ngOnDestroy(): void {}
}
