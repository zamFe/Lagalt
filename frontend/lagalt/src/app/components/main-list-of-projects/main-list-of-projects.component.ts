import { Component, OnInit } from '@angular/core';

// TEMP MODEL OF PROJECT
interface Project {
  id: number,
  name: string,
  type: string,           // could be enum instead possibly
  thumbnail: string,      // only a background color for now
  //members: string[]
}

let projectInit: Project[] = [
  {id: 1, name: "test1", type: "Music", thumbnail: "red"},
  {id: 2, name: "test2", type: "Game Dev", thumbnail: "blue"},
  {id: 3, name: "test3", type: "Web Dev", thumbnail: "green"},
  {id: 4, name: "test4", type: "Film", thumbnail: "yellow"},
  
]

@Component({
  selector: 'app-main-list-of-projects',
  templateUrl: './main-list-of-projects.component.html',
  styleUrls: ['./main-list-of-projects.component.css']
})
export class MainListOfProjectsComponent implements OnInit {

  public dummyProjectList: Project[] = []
  constructor() { }

  ngOnInit(): void {
    this.dummyProjectList = projectInit;
  }

}
