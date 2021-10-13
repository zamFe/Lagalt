import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model'
import { NgForm, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.css']
})
export class ProfilePage implements OnInit, OnDestroy {

  private user$: Subscription;
  public user: User = {
    id: 0,
    userName: '',
    description: '',
    image: '',
    portfolio: '',
    skills: [],
    projects: []
  }
  public isChecked = true;
  public color = 'accent';

  constructor(private readonly userService : UserService) {
    this.user$ = this.userService.user$.subscribe((user: User) => {
      this.user = user;
    })
  }

  ngOnInit(): void {
    this.userService.getUserById(1) // CHANGE TO IMPLEMENT ON LOGIN
  }
  ngOnDestroy(): void {
    this.user$.unsubscribe();
  }

  //Changes the hidden mode button from true to false or false to true
  changed(){
    console.log(this.isChecked);
  }


  //Adds skill to a list in the user profile
  addSkill(addSkillForm : NgForm){

    // WAIT WITH IMPLEMENTING TILL USER MODEL IS UPDATED with skill[] instead of array of ids

    // if(addSkillForm){
    //   this.skills.push(addSkillForm.value.skills);
    // }
    // let usersResults : User[] = []
    // this.userService.getUsers$().subscribe(data => {
    //   console.log(data);
    //   usersResults = data
    // })
    // let usersResultsById : User
    // this.userService.getUserById$().subscribe(data => {
    //   console.log(data);
    //   usersResultsById = data
    //   let description = data.description
    //   console.log(description);
    // })
  }

  handleDescription(handleDescriptionForm : NgForm){
    // maybe check that description is changed, and that description is not equal to empty string or white spaces
    this.user.description = handleDescriptionForm.value.description
    // RUN PUT USER API CALL so we can update description
  }


}
