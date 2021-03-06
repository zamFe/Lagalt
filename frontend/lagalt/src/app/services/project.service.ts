import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Project, PutProject } from "../models/project.model";
import { BehaviorSubject, Subscription } from 'rxjs';
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
    source: "",
    administratorIds: []
}


@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    private _loading: boolean = false;
    private _recommended: boolean = false; 

    // Store observables
    public readonly projects$: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);
    public readonly project$: BehaviorSubject<Project> = new BehaviorSubject<Project>(defaultProject);
    
    // Page properties
    public offset = 0;
    public limit = 10;
    public pages = 0;
    public currentPage = 1;
    public totalEntities = 0;

    // Filter properties
    public professionId = 0;
    public keyword = "";

    constructor(private readonly http : HttpClient) {
    }

    // Getters
    get loading(): boolean {
        return this._loading;
    }
    get recommended(): boolean {
        return this._recommended;
    }

    // State CRUD functions
    public setProjects(projects: Project[]): void {
        this.projects$.next(projects)
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

    // Page modifiers
    public nextPage(): void {
        this.offset += this.limit;
        this.currentPage++;
        this.getProjects();
        
    }
    public prevPage(): void {
        this.offset -= this.limit;
        this.currentPage--;
        this.getProjects();
    }

    // Filter modifier
    public filterSearchByProfession(professionId: number) {
        this.professionId = professionId;
    }

    // API CRUD calls
    public getProjects(): Subscription {
        this._recommended = false;
        this._loading = true;
        return this.http.get<ProjectPageWrapper>
        (`${API_URL}?offset=${this.offset}&limit=${this.limit}&professionId=${this.professionId}&keyword=${this.keyword}`)
            .subscribe((page: ProjectPageWrapper) => {
                this.setProjects(page.results)
                this.totalEntities = page.totalEntities;
                this.pages = Math.ceil(this.totalEntities/this.limit)
                this._loading = false;
            },
            (error: HttpErrorResponse) => {
                alert(error.status + " : " + error.statusText)
            });
    }

    public getProjectById(id: number): Subscription {
        this._loading = true;
        return this.http.get<Project>(`${API_URL}/${id}`)
            .subscribe((project: Project) => {
                this._loading = false;
                this.setProject(project)
            },
            (error: HttpErrorResponse) => {
                alert(error.status + " : " + error.statusText)
            });
    }

    public getRecommendedProjectsByUserId(userId: number): Subscription {
        this._recommended = true;
        this._loading = true;
        return this.http.get<ProjectPageWrapper>(`${API_URL}/Recommended/${userId}`)
            .subscribe((page: ProjectPageWrapper) => {
                this.setProjects(page.results)
                this._loading = false;
            },
            (error: HttpErrorResponse) => {
                alert(error.status + " : " + error.statusText)
            })
    }

    public postProject(project: Object): Subscription {
        this._loading = true;
        return this.http.post<Project>(API_URL, project)
            .subscribe((response: Project) => {
                this.addProject(response)
                this._loading = false;
            },
            (error: HttpErrorResponse) => {
                alert(error.status + " : " + error.statusText)
            });
    }

    public putProject(project: PutProject): Subscription {
        this._loading = true;
        this.removeProject(project.id)
        return this.http.put<Project>(`${API_URL}/${project.id}`, project)
        .subscribe((response: Project) => {
            this.addProject(response)
            this._loading = false;
        },
        (error: HttpErrorResponse) => {
            alert(error.status + " : " + error.statusText)
        });
    }
}
