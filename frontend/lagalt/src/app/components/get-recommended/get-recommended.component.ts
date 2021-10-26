import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-get-recommended',
  templateUrl: './get-recommended.component.html',
  styleUrls: ['./get-recommended.component.css']
})
export class GetRecommendedComponent implements OnInit {

  public userId: number = 0;
  constructor(private readonly projectService: ProjectService, private readonly userService: UserService) { 
    this.userService.user$.subscribe(user => this.userId = user.id)
  }

  ngOnInit(): void {
  }

  getRecommended() {
    this.projectService.getRecommendedProjectsByUserId(this.userId)
  }

}
