import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/profile.model'
import { NgForm, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.css']
})
export class ProfilePage implements OnInit {

  public skills : [string] = ['Using LagAlt']
  public isChecked = true;
  public color = 'accent';
  public description : string = ""

  constructor(private readonly userService : UserService) {


  }

  ngOnInit(): void {
    this.userService.getUsers()
    this.userService.getUserById(1)
    this.userById$.subscribe(data => {
      this.description = data.description
    })
  }
  //Changes the hidden mode button from true to false or false to true
  changed(){
    console.log(this.isChecked);
  }
  //Adds skill to a list in the user profile
  addSkill(addSkillForm : NgForm){
    if(addSkillForm){
      this.skills.push(addSkillForm.value.skills);
    }
    let usersResults : User[] = []
    this.userService.getUsers$().subscribe(data => {
      console.log(data);
      usersResults = data
    })
    let usersResultsById : User
    this.userService.getUserById$().subscribe(data => {
      console.log(data);
      usersResultsById = data
      let description = data.description
      console.log(description);

    })
  }



  handleDescription(handleDescriptionForm : NgForm){
    let newDescription = handleDescriptionForm.value.description
    this.userService.getUserById$().subscribe(data => {
      data.description = newDescription

    })
    //this.userService.putUserById$(textAreaForm.value.description)
    // let userDescription = this.userService.getUserById$().subscribe(data => {
    //   data.description
    //   console.log("USR ",userDescription);

    // })
  }


  get users$(): Observable<User[]> {
    return this.userService.getUsers$();
  }
  get userById$(): Observable<User> {
    return this.userService.getUserById$();
  }

}
