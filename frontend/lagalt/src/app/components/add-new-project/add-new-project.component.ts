import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Profession } from 'src/app/models/profession.model';
import { ProfessionService } from 'src/app/services/profession.service';
import { ProjectService } from 'src/app/services/project.service';

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

  constructor(private readonly professionService: ProfessionService, 
    private readonly projectService: ProjectService,
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
    // ADD CURRENT USER as well, implement user service and add user object
    let selectedProfessionId: number = this.professions.find(e => e.name === this.createProjectForm.value.profession)!.id
    let newProject = {
      title: this.createProjectForm.value.title,
      description: this.createProjectForm.value.description,
      image: this.createProjectForm.value.imageUrl,
      profession: selectedProfessionId
      // ADD USER HERE
    }
    console.log(newProject)
    //this.projectService.postProject(newProject)   // Create a postProject method in projectService
    this.modalOpen = false;
  }

  ngOnInit(): void {
    this.professionService.getProfessions();
  }

  ngOnDestroy(): void {
    this.professions$.unsubscribe();
  }

}
