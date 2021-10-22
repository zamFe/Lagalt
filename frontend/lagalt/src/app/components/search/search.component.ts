import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from "../../models/project.model";
import { ProjectService } from "../../services/project.service"

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnDestroy {


  constructor(private readonly projectService : ProjectService, private router: Router) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  searchForProject(searchProjectForm : NgForm) : void{
    this.handleRouteChange()
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.toLowerCase().includes(searchProjectForm.value.searchInput.toLowerCase()))
    }).unsubscribe()
    this.projectService.setProjects(searchResults)
  }

  searchMusic(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 1)
    }).unsubscribe()
    this.projectService.setProjects(searchResults)
  }

  searchFilm(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 2)
    }).unsubscribe()
    this.projectService.setProjects(searchResults)
  }

  searchGame(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 3)
    }).unsubscribe()
    this.projectService.setProjects(searchResults)
  }

  searchWeb(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput) && p.profession.id === 4)
    }).unsubscribe()
    this.projectService.setProjects(searchResults)
    
    
  }
  
  handleRouteChange() {
    if (this.router.url !== "main" && this.router.url !== "my-projects") {
      this.router.navigate(['main'])
    }
  }
}
