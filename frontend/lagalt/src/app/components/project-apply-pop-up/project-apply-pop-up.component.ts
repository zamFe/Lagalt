import {Component} from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-project-apply-pop-up',
  templateUrl: './project-apply-pop-up.component.html',
  styleUrls: ['./project-apply-pop-up.component.css']
})
export class ProjectApplyPopUpComponent {
  closeResult = '';

  projectName: string = "Project name"
  public textAreaForm: FormGroup;

  constructor(private modalService: NgbModal, private fb : FormBuilder) {
    this.textAreaForm = fb.group({
      textArea: ""
    });
  }

  open(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'});
  }

}
