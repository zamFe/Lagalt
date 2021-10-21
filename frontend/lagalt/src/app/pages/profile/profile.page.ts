import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserComplete } from 'src/app/models/user/user-complete.model'
import { NgForm, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';
import { UserService } from 'src/app/services/user.service';
import { SkillService } from 'src/app/services/skill.service';
import { Observable, Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { Skill } from 'src/app/models/skill.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.css']
})
export class ProfilePage implements OnInit, OnDestroy {
  public profileJson: string = '';
  private user$: Subscription;
  private skills$: Subscription;
  public user: UserComplete = {
    id: 0,
    username: '',
    description: '',
    image: '',
    portfolio: '',
    skills: [],
    projects: []
  }

  public skill: Skill = {
    id: 0,
    name: ''
  }
  public isChecked = true;
  public color = 'accent';
  public skills : Skill[] = []
  

  constructor(private readonly userService : UserService, public auth: AuthService, 
    private readonly skillService : SkillService) {
    this.user$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user = user;
    })

    this.skills$ = this.userService.user$.subscribe((user: UserComplete) => {
      this.user.skills = user.skills
    })
    
    
  }

  ngOnInit(): void {
    this.auth.user$.subscribe(
      (profile) => {
        (this.user.username = JSON.parse(JSON.stringify(profile?.nickname, null, 2)))
        console.log(this.user);
        
        this.userService.userExists(this.user.username).pipe(finalize(() => {    
        })).subscribe(res => {    
          if(res){
            this.userService.getUserByUsername(this.user.username)
         }
        }, () => {    
          this.userService.postUserByUsername(this.user.username)
        });  
        console.log(profile)
      }
    );
    this.auth.idTokenClaims$.subscribe(data =>
      console.log(data)) 

      this.userService.getUserById(2)
      console.log("Skills" + this.user.skills);
      

      //console.log("Skills" + this.user.skills[1]);
    //this.userService.getUserById(1) // CHANGE TO IMPLEMENT ON LOGIN

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

    this.skill.name = addSkillForm.value;
    console.log(this.skill.name)

    //Spør hvorfor man må ha user og project når man adder ny skill
    
    this.skillService.postSkill(this.skill)

    //this.skillService.addSkill(addSkillForm)
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
