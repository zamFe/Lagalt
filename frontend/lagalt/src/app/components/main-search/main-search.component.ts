import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Project } from "../../models/project.model";
import { ProjectService } from "../../services/project.service"

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

  constructor(private readonly projectService : ProjectService) { }

  ngOnInit(): void {
  }

  searchForProject(searchProjectForm : NgForm) : void{

  }
}
