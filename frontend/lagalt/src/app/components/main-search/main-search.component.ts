import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Project } from "../../models/project.model";
import { ProjectService } from "../../services/project.service"
// let projectInit: Project[] = [
//   {id: 1, name: "test1", type: "Music", thumbnail: "red"},
//   {id: 2, name: "test2", type: "Game Dev", thumbnail: "blue"},
//   {id: 3, name: "test3", type: "Web Dev", thumbnail: "green"},
//   {id: 4, name: "test4", type: "Film", thumbnail: "yellow"},

// ]
let projectInit: Project[] = [{
  id: 0,
  profession: 0,
  title: '',
  image: '',
  skills: []
}]

@Component({
  selector: 'app-main-search',
  templateUrl: './main-search.component.html',
  styleUrls: ['./main-search.component.css']
})
export class MainSearchComponent implements OnInit {

  public specificProject : Project[] = [];
  public dummyProjectList: Project[] = []

  constructor(private readonly projectService : ProjectService) { }

  ngOnInit(): void {
    this.dummyProjectList = projectInit;
  }

  searchForProject(searchProjectForm : NgForm) : void{
    console.log(this.projectService.fetchProjects())


    // this.specificProject = this.dummyProjectList.filter(project => {
    //   return project.title.includes(searchProjectForm.value.projectName)
    // })
  }
}
