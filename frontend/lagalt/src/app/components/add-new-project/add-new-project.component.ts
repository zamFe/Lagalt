import { Component, OnDestroy, OnInit, ɵɵqueryRefresh } from '@angular/core';
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
      // this.professions = professions;
    })
    this.professions = [{id: 1, name: 'Music'}, 
                        {id: 2, name: 'Film'}, 
                        {id: 3, name: 'Spill'}, 
                        {id: 4, name: 'Web'}]
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
    let userId = Number.parseInt(JSON.parse(localStorage.getItem('userId')!))
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
    this.createProjectForm.value.title = "";
    this.createProjectForm.value.description = "";
    this.createProjectForm.value.imageUrl = "";
    this.modalOpen = false;
    setTimeout(function() {
        window.location.reload();
      }, 1000)
    

  }

  ngOnInit(): void {
    this.professionService.getProfessions();
  }

  ngOnDestroy(): void {
    this.professions$.unsubscribe();
  }

}
