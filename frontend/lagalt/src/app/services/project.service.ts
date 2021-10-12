import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Project } from "../models/project.model";
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";

const API_URL = environment.apiUrl;

@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    // Private store varaibles
    private readonly _projects$: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);
    private readonly _renderProjects$: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);

    constructor(private readonly http : HttpClient) {
    }

    // State CRUD functions
    public getProjects$(): Observable<Project[]> {
        return this._projects$.asObservable()
    }
    public getRenderProjects$(): Observable<Project[]> {
      return this._renderProjects$.asObservable()
    }

    public setProjects(projects: Project[]): void {
        this._projects$.next(projects)
    }
    public setRenderProjects(projects: Project[]): void {
        this._renderProjects$.next(projects)
    }

    public addProject(project: Project): void {
        const projects = [...this._projects$.getValue(), project]
        this.setProjects(projects)
    }

    public removeProject(project: Project): void {
        const projects = this._projects$.getValue().filter(p => p.id !== project.id);
        this.setProjects(projects)
    }

    // API CRUD calls
    public getProjects(): Subscription {

        //set professions as enum in storage here.
        return this.http.get<Project[]>(`${API_URL}Projects`)
            .subscribe((projects: Project[]) => {
                this.setProjects(projects)
                this.setRenderProjects(projects)
            });
    }

    // post, put/id, get/id
}
