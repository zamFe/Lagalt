import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Project } from "../../models/project.model";
import { ProjectService } from "../../services/project.service"
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-main-search',
  templateUrl: './main-search.component.html',
  styleUrls: ['./main-search.component.css']
})
export class MainSearchComponent implements OnInit {

  constructor(private readonly projectService : ProjectService) { }

  public specificProject = [];

  ngOnInit(): void {

  }

  searchForProject(searchProjectForm : NgForm) : void{
    let searchResults : Project[] = []
    this.projectService.getProjects$().subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput))
    })
    this.projectService.setRenderProjects(searchResults)


  }
  get projects$(): Observable<Project[]> {
    return this.projectService.getRenderProjects$();
  }
}
