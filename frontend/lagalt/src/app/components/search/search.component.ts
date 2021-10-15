import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Project } from "../../models/project.model";
import { ProjectService } from "../../services/project.service"

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnDestroy {

  constructor(private readonly projectService : ProjectService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  searchForProject(searchProjectForm : NgForm) : void{
    let searchResults : Project[] = []
    // OPTIONAL: USE REGEX AND MATCH INSTEAD OF INCLUDES to account for captial/lower case searches
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput))
    }).unsubscribe()
    this.projectService.setRenderProjects(searchResults)
  }

  searchMusic(searchProjectForm : NgForm): void {
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 1)
    }).unsubscribe()
    this.projectService.setRenderProjects(searchResults)
  }

  searchFilm(searchProjectForm : NgForm): void {
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 2)
    }).unsubscribe()
    this.projectService.setRenderProjects(searchResults)
  }

  searchGame(searchProjectForm : NgForm): void {
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 3)
    }).unsubscribe()
    this.projectService.setRenderProjects(searchResults)
  }

  searchWeb(searchProjectForm : NgForm): void {
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 4)
    }).unsubscribe()
    this.projectService.setRenderProjects(searchResults)
  }
  
}
