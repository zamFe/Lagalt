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

  searchForProject(professionId: number) : void {
    this.handleRouteChange();
    this.handleFilter(professionId);
    this.handlePageReset();


    if (this.searchField !== "") {
      this.projectService.keyword = this.searchField;
    }
    this.projectService.getProjects();
    this.searchField = "";
    this.projectService.keyword = "";
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

  handlePageReset() {
    this.projectService.offset = 0;
    this.projectService.currentPage = 1;
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }
}
