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
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.includes(searchProjectForm.value.searchInput))
    }).unsubscribe()
    this.projectService.setRenderProjects(searchResults)
  }

  searchAll(): void {
    console.log("all")
  }

  searchMusic(): void {
    console.log("music")
  }

  searchFilm(): void {
    console.log("film")
  }

  searchGame(): void {
    console.log("game")
  }

  searchWeb(): void {
    console.log("web")
  }
  
}
