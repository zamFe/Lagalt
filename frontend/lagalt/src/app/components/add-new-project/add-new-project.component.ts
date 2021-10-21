import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Profession } from 'src/app/models/profession.model';
import { Project } from 'src/app/models/project.model';
import { ProfessionService } from 'src/app/services/profession.service';
import { ProjectService } from 'src/app/services/project.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit, OnDestroy {


  
  private professions$: Subscription
  public professions: Profession[] = []
  public createProjectForm: FormGroup
  public modalOpen: boolean = false;
  public clicked: boolean = false;

  constructor(private readonly professionService: ProfessionService, 
    private readonly projectService: ProjectService,
    private readonly userService: UserService,
    private modalService: NgbModal,
    private fb : FormBuilder) {
    this.professions$ = this.professionService.professions$.subscribe((professions: Profession[]) => {
      this.professions = professions;
    })
    this.createProjectForm = fb.group({
      title: "",
      imageUrl: "",
      profession: "Music",
      description: ""
    });
  }

  open(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'});
    this.modalOpen = true;
  }

  submitForm() {
    let userId = 0;
    this.userService.user$.subscribe(user => userId = user.id).unsubscribe();
    let selectedProfessionId: number = this.professions.find(e => e.name === this.createProjectForm.value.profession)!.id
    let newProject: Object = {
      title: this.createProjectForm.value.title,
      description: this.createProjectForm.value.description,
      image: this.createProjectForm.value.imageUrl,
      professionId: selectedProfessionId,
      users: [userId],
      administratorIds: [userId],
      progress: 'founding',
      source: null,
      skills: []
    }
    this.projectService.postProject(newProject)

    this.modalOpen = false;
  }

  ngOnInit(): void {
    this.professionService.getProfessions();
  }

  ngOnDestroy(): void {
    this.professions$.unsubscribe();
  }

}
