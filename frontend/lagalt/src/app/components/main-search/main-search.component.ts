import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Project } from '../main-list-of-projects/main-list-of-projects.component';

let projectInit: Project[] = [
  {id: 1, name: "test1", type: "Music", thumbnail: "red"},
  {id: 2, name: "test2", type: "Game Dev", thumbnail: "blue"},
  {id: 3, name: "test3", type: "Web Dev", thumbnail: "green"},
  {id: 4, name: "test4", type: "Film", thumbnail: "yellow"},

]

@Component({
  selector: 'app-main-search',
  templateUrl: './main-search.component.html',
  styleUrls: ['./main-search.component.css']
})
export class MainSearchComponent implements OnInit {

  public specificProject : Project[] = [];
  public dummyProjectList: Project[] = []

  constructor() { }

  ngOnInit(): void {
    this.dummyProjectList = projectInit;
  }

  searchForProject(searchProjectForm : NgForm) : void{

    this.specificProject = this.dummyProjectList.filter(project => {
      return project.name.includes(searchProjectForm.value.projectName)
    })
  }
}
