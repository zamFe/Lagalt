import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Profession } from 'src/app/models/profession.model';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {


  public textAreaForm: FormGroup
  public professions: Profession[] = []

  constructor(private modalService: NgbModal, private fb : FormBuilder) {
    this.textAreaForm = fb.group({
      textArea: ""
    });
  }

  open(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'});
  }

  ngOnInit(): void {
  }

}
