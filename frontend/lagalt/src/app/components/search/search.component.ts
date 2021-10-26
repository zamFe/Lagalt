import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { Router } from '@angular/router';
import { Project } from "../../models/project.model";
import { ProjectService } from "../../services/project.service"

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnDestroy {


  public searchField = "";
  public professionId = 0;
  constructor(private readonly projectService : ProjectService,
    private router: Router) {
  }

  searchForProject(searchProjectForm : NgForm) : void {
    // run get call which includes searchResults instead
    // if search field is empty do normal getProjects
    // else, getProjects(projectTitle)
    
    //this.projectService.getProjects()

    let searchResults : Project[] = []
    this.projectService.projects$.subscribe(data => {
      searchResults = data.filter(p => p.title.toLowerCase().includes(searchProjectForm.value.searchInput.toLowerCase()))
    }).unsubscribe()
    searchResults.forEach(el => console.log(el))
    this.projectService.setProjects(searchResults)

    this.searchField = "";

  }
  
  searchAll(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    this.handleFilter(0)
    this.searchForProject(searchProjectForm)
  }

  searchMusic(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    this.handleFilter(1)
    this.searchForProject(searchProjectForm)
  }

  searchFilm(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    this.handleFilter(2)
    this.searchForProject(searchProjectForm)
  }

  searchGame(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    this.handleFilter(3)
    this.searchForProject(searchProjectForm)
  }

  searchWeb(searchProjectForm : NgForm): void {
    this.handleRouteChange()
    this.handleFilter(4)
    this.searchForProject(searchProjectForm)
  }

  handleRouteChange() {
    if (this.router.url !== "/main") {
      this.router.navigate(['main'])
    }
  }

  handleFilter(professionId: number) {
    this.professionId = professionId
    this.projectService.filterSearchByProfession(professionId)
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }
}
