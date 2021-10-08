import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Project } from "../models/project.model";
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";

const apiURL = "";

@Injectable({
    providedIn: 'root'
})
export class ProjectService {

    private test: Project = {
        id: 0,
        profession: 0,
        title: "",
        image: "",
        skills: []
    }
    private projects: Project[] = [];
    private error: string = "";

    constructor(private readonly http: HttpClient) {
    }

    public fetchProjects() {


        // this.http.get(apiURL).pipe(

        // )
    }

}