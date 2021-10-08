import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Project, ProjectDetailed } from "../models/project.model";
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";

const apiURL = "";
const apiFetchProjects = "";

@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    private projects: Project[] = [];
    private error: string = "";

    constructor(private readonly http: HttpClient) {
    }

    public fetchProjects() {
        this.http.get<ProjectDetailed>(apiFetchProjects).pipe(
            map(element => {
                let tempProject: Project = {
                    id: element.id,
                    profession: element.profession,
                    title: element.title,
                    image: element.image,
                    skills: element.skills
                }
                this.projects.push(tempProject)
            })
        )
        console.log(this.projects);
    }
}