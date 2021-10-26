import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Application, ApplicationResponse, PutApplication } from '../models/application.model';

const API_URL = `${environment.apiUrl}Applications`;

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private _loading: boolean = false;

  public readonly applications$: BehaviorSubject<Application[]> = new BehaviorSubject<Application[]>([]);

  constructor(private readonly http: HttpClient) { }

  public setApplications(applications: Application[]): void {
    this.applications$.next(applications)
  }

  public addApplication(application: Application): void {
    const applications = [...this.applications$.getValue(), application]
    this.setApplications(applications)
  }

  public getApplicationsByProjectId(projectId: number): Subscription {
    this._loading = true;
    return this.http.get<ApplicationResponse>(`${API_URL}/Project/${projectId}`)
        .subscribe((applications: ApplicationResponse) => {
          this._loading = false;
            this.setApplications(applications.results)
        },
        (error: HttpErrorResponse) => {
            //console.log(error.message);
            alert(error.status + " : " + error.statusText)
        });
  }

  public postApplication(application: Object): Subscription {
    this._loading = true;
    return this.http.post<Application>(API_URL, application)
      .subscribe((application: Application) => {
        this._loading = false;
        this.addApplication(application)
      },
      (error: HttpErrorResponse) => {
          //console.log(error.message);
          alert(error.status + " : " + error.statusText)
      })
  }
  public putApplication(application: PutApplication): Subscription {
    this._loading = true;
    return this.http.put<Application>(`${API_URL}/${application.id}`, application)
      .subscribe((application: Application) => {
        this._loading = false;
        this.addApplication(application)
      },
      (error: HttpErrorResponse) => {
          //console.log(error.message);
          alert(error.status + " : " + error.statusText)
      })
  }

}
