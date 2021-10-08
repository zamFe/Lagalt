import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/models/profile.model'
import { NgForm, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.css']
})
export class ProfilePage implements OnInit {

  public skills : [string] = ['Using LagAlt']
  public textAreaForm: FormGroup;
  public isChecked = true;
  public color = 'accent';

  constructor(private fb : FormBuilder) {

    this.textAreaForm = fb.group({
      textArea: ""
    });
  }



  ngOnInit(): void {
  }
  //Changes the hidden mode button from true to false or false to true
  changed(){
    console.log(this.isChecked);
  }
  //Adds skill to a list in the user profile
  addSkill(addSkillForm : NgForm){
    console.log(this.skills);

    if(addSkillForm){
      this.skills.push(addSkillForm.value.skills);
    }
  }

  addDescription(newDescription: string){
    if(newDescription){

    }
  }

}
