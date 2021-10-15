import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/models/profile.model'
import { NgForm, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.css']
})
export class ProfilePage implements OnInit {
  public profileJson: string = '';
  public skills : [string] = ['Using LagAlt']
  public textAreaForm: FormGroup;
  public isChecked = true;
  public color = 'accent';
  

  constructor(private fb : FormBuilder, public auth: AuthService) {

    this.textAreaForm = fb.group({
      textArea: ""
    });

    
  }



  ngOnInit(): void {
    this.auth.user$.subscribe(
      (profile) => (this.profileJson = JSON.stringify(profile, null, 2))
    );
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
