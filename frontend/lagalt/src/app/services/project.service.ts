import { Injectable} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Project } from "../models/project.model";
import { BehaviorSubject, Subscription } from 'rxjs';
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { ProjectPageWrapper } from "../models/project-page-wrapper.model";

const API_URL = `${environment.apiUrl}Projects`;
const defaultProject: Project = {
    id: 0,
    profession: {id : 0, name : ""},
    title: "",
    image: "",
    skills: [],
    users: [],
    description: "",
    progress: "",
    source: null,
    administratorIds: []
}


@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    // Store varaibles
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

    public removeProject(projectId: number): void {
        const projects = this.projects$.getValue().filter(p => p.id !== projectId);
        this.setProjects(projects)
    }

    // API CRUD calls
    public getProjects(): Subscription {
        return this.http.get<ProjectPageWrapper>(API_URL)
            .subscribe((page: ProjectPageWrapper) => {
                this.setProjects(page.results)
                if (this.renderProjects$.value.length < page.results.length) {
                    this.setRenderProjects(page.results)
                }
               
            });
    }

    
    public getProjectsByUserId(userId : number): Subscription {
        return this.http.get<ProjectPageWrapper>(`${API_URL}/User/${userId}`)
            .subscribe((project: ProjectPageWrapper) => {
                this.setProjects(project.results)
                this.setRenderProjects(project.results)
            });
    }


    public getProjectById(id: number): Subscription {
        return this.http.get<Project>(`${API_URL}/${id}`)
            .subscribe((project: Project) => {
                this.setProject(project)
            });
    }

    public getRecommendedProjectsByUserId(userId: number): Subscription {
        return this.http.get<ProjectPageWrapper>(`${API_URL}/Recommended/${userId}`)
            .subscribe((page: ProjectPageWrapper) => {
                this.setRenderProjects(page.results)
            })
    }

    public postProject(project: Object): Subscription {
        return this.http.post<Project>(API_URL, project)
            .subscribe((response: Project) => {
                this.addProject(response)
            });
    }

    // IKKE TESTET
    public putProject(project: Project): Subscription {
        this.removeProject(project.id)
        return this.http.put<Project>(`${API_URL}/${project.id}`, project)
        .subscribe((response: Project) => {
            this.addProject(response)
        });
    }
}
