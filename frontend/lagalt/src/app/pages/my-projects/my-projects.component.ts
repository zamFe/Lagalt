import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Project } from 'src/app/models/project.model';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { MessageService } from 'src/app/services/message.service';
import { ProjectService } from 'src/app/services/project.service';
import { SkillService } from 'src/app/services/skill.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html',
  styleUrls: ['./my-projects.component.css']
})
export class MyProjectsComponent implements OnInit{

  private user$ : Subscription
  public user!: UserComplete;

  constructor(
    private readonly projectService: ProjectService,
    private readonly skillsService: SkillService,
    private readonly messageService : MessageService,
    private readonly userService : UserService) {

      this.user$ = this.userService.user$.subscribe((user : UserComplete) =>{
        this.user = user
      })

   }

  ngOnInit(): void {
    this.projectService.getProjectsByUserId(this.user.id);
    this.skillsService.getSkills();
    this.messageService.getMessagesByProjectId(this.user.id)

  }


}
