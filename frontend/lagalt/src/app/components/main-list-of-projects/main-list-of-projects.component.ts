import { Component, OnInit } from '@angular/core';
import { Project } from "../../models/project.model";

// let projectInit: Project[] = [
//   {id: 1, name: "test1", type: "Music", thumbnail: "red"},
//   {id: 2, name: "test2", type: "Game Dev", thumbnail: "blue"},
//   {id: 3, name: "test3", type: "Web Dev", thumbnail: "green"},
//   {id: 4, name: "test4", type: "Film", thumbnail: "yellow"},
// ]

@Component({
  selector: 'app-main-list-of-projects',
  templateUrl: './main-list-of-projects.component.html',
  styleUrls: ['./main-list-of-projects.component.css']
})
export class MainListOfProjectsComponent implements OnInit {
  projectInit: Project = {
    id: 0,
    profession: 0,
    title: '',
    image: '',
    skills: []
  };
  public dummyProjectList: Project[] = []
  constructor() { }

  ngOnInit(): void {
    this.dummyProjectList = [this.projectInit];
  }

}
