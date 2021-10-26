import { Injectable} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Project } from "../models/project.model";
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
    source: null,
    administratorIds: []
}


@Injectable({
    providedIn: 'root'
})
export class ProjectService {

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

    public nextPageRecommendedProjects(userId: number): void {
        this.offset += this.limit;
        this.currentPage++;
        this.getRecommendedProjectsByUserId(userId);
    }
    public prevPageRecommendedProjects(userId: number): void {
        this.offset -= this.limit;
        this.currentPage--;
        this.getRecommendedProjectsByUserId(userId);
    }

    // Filter modifier
    public filterSearchByProfession(professionId: number) {
        this.professionId = professionId;
    }


    // API CRUD calls
    public getProjects() {
        return this.http.get<ProjectPageWrapper>
        (`${API_URL}?offset=${this.offset}&limit=${this.limit}&professionId=${this.professionId}&keyword=${this.keyword}`)
        .subscribe((page: ProjectPageWrapper) => {
            this.setProjects(page.results)
            this.totalEntities = page.totalEntities;
            this.pages = Math.ceil(this.totalEntities/this.limit)
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
                this.setProjects(page.results)
                this.pages = Math.ceil(this.totalEntities/this.limit)
            })
    }

    public postProject(project: Object): Subscription {
        return this.http.post<Project>(API_URL, project)
            .subscribe((response: Project) => {
                this.addProject(response)
            });
    }

    public putProject(project: Project): Subscription {
        this.removeProject(project.id)
        return this.http.put<Project>(`${API_URL}/${project.id}`, project)
        .subscribe((response: Project) => {
            this.addProject(response)
        });
    }
}
