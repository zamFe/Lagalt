import { Component, OnInit } from '@angular/core';
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

  public userId: number = 0;
  public projectId: number = 0;
  private userIdsInProject: number[] = [];
  constructor(private readonly projectService: ProjectService,
    private readonly skillsService: SkillService,
    private readonly messageService : MessageService) {
      this.projectService.project$.subscribe(data => {
        this.projectId = data.id
        this.userIdsInProject = data.users.map(u => {
          return u.id
      })
    })
  }

  ngOnInit(): void {
    this.userId = Number(localStorage.getItem('userId'))
    this.projectService.getProjectsByUserId(this.userId);
    this.skillsService.getSkills();
    
    // CHECK IF USER IS IN PROJECT
    if (this.userIdsInProject.includes(this.userId)) {
      this.messageService.getMessagesByProjectId(this.projectId)
    }
    
  }


}
