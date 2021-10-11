import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Project, ProjectDetailed } from "../models/project.model";
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";

const apiURL = "";
const apiFetchProjects = "https://localhost:44348/api/Projects";
@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    private projects: Project[] = [];
    private error: string = "";

    constructor(private readonly http : HttpClient) {
    }

    public async fetchProjects() {
        console.log(await this.http.get("https://localhost:44348/api/Messages").toPromise())
        return await this.http.get<ProjectDetailed[]>(apiFetchProjects).toPromise()
        // return this.http.get<ProjectDetailed>(apiFetchProjects).pipe(
        //     map(element => {
        //         let tempProject: Project = {
        //             id: element.id,
        //             profession: element.profession,
        //             title: element.title,
        //             image: element.image,
        //             skills: element.skills
        //         }
        //         console.log(tempProject);

        //     })
        // )
    }
}
