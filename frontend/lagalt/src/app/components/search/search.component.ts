import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Project } from "../../models/project.model";
import { ProjectService } from "../../services/project.service"
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  private projects$: Subscription;
  public projects: Project[] = [];
  constructor(private readonly projectService : ProjectService) {
    this.projects$ = this.projectService.projects$.subscribe((projects: Project[]) => {
      this.projects = projects
    })
  }

  public specificProject = [];

  ngOnInit(): void {

  }

  searchForProject(searchProjectForm : NgForm) : void{
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput))
    })
    this.projectService.setRenderProjects(searchResults)


  }
}
