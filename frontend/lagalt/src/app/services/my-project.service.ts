import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Project } from "../models/project.model";
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from "src/environments/environment";
import { ProjectPageWrapper } from "../models/project-page-wrapper.model";

const API_URL = `${environment.apiUrl}Projects`;
@Injectable({
  providedIn: 'root'
})
export class MyProjectService {

  private _loading: boolean = false;
  // Store observables
  public readonly myProjects$: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);

  // Page properties
  public offset = 0;
  public limit = 10;
  public pages = 0;
  public currentPage = 1;
  public totalEntities = 0;

  constructor(private readonly http : HttpClient) {
  }

  // State CRUD functions
  public setMyProjects(projects: Project[]): void {
      this.myProjects$.next(projects)
  }

  // Page modifiers
  nextPage(userId: number): void {
      this.offset += this.limit;
      this.currentPage++;
      this.getProjectsByUserId(userId);
  }
  prevPage(userId: number): void {
      this.offset -= this.limit;
      this.currentPage--;
      this.getProjectsByUserId(userId);
  }

  // API CRUD calls
  public getProjectsByUserId(userId : number): Subscription {
      this._loading = true;
      return this.http.get<ProjectPageWrapper>(`${API_URL}/User/${userId}?offset=${this.offset}&limit=${this.limit}`)
          .subscribe((project: ProjectPageWrapper) => {
              this.setMyProjects(project.results)
              this.totalEntities = project.totalEntities;
              this.pages = Math.ceil(this.totalEntities/this.limit)
              this._loading = false;
          },
          (error: HttpErrorResponse) => {
              alert(error.status + " : " + error.statusText)
          });
  }

  get loading(): boolean {
    return this._loading;
}
}
