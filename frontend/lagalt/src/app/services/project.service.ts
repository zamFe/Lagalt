import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Project } from "../models/project.model";
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";

const API_URL = `${environment.apiUrl}Projects`;
const defaultProject: Project = {
    id: 0,
    profession: 0,
    title: "",
    image: "",
    skills: [],
    messages: [],
    users: [],
    description: "",
    progress: "",
    source: null
}


@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    // Private store varaibles
    public readonly projects$: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);
    public readonly renderProjects$: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);
    public readonly project$: BehaviorSubject<Project> = new BehaviorSubject<Project>(defaultProject);

    constructor(private readonly http : HttpClient) {
    }

    // State CRUD functions

    public setProjects(projects: Project[]): void {
        this.projects$.next(projects)
    }
    public setRenderProjects(projects: Project[]): void {
        this.renderProjects$.next(projects)
    }
    public setProject(project: Project): void {
        this.project$.next(project)
    }

    public addProject(project: Project): void {
        const projects = [...this.projects$.getValue(), project]
        this.setProjects(projects)
    }

    public removeProject(project: Project): void {
        const projects = this.projects$.getValue().filter(p => p.id !== project.id);
        this.setProjects(projects)
    }

    // API CRUD calls
    public getProjects(): Subscription {

        //set professions as enum in storage here.
        return this.http.get<Project[]>(API_URL)
            .subscribe((projects: Project[]) => {
                this.setProjects(projects)
                this.setRenderProjects(projects)
            });
    }

    public getProjectById(id: number): Subscription {
        return this.http.get<Project>(`${API_URL}/${id}`)
            .subscribe((project: Project) => {
                this.setProject(project)
            });
    }

    // public postProject(project: Project): Subscription {
    //     return this.http.post<Project>(API_URL, project)
    //         .subscribe((response) => {
    //             console.log("response:")
    //             console.log(response)
    //             this.addProject(project)
    //             console.log("project added to state:")
    //             console.log(this._projects$)
    //             console.log("project added to API go check")
    //         });
    // }

    // post, put/id, get/id
}
