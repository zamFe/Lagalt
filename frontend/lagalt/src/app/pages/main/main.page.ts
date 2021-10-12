import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.css']
})
export class MainPage implements OnInit {

  constructor(private readonly projectService: ProjectService) { }

  ngOnInit(): void {
    this.projectService.getProjects();
  }

}
