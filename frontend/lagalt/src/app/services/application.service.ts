import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Application } from '../models/application.model';

const API_URL = `${environment.apiUrl}Applications`;

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

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
    return this.http.get<Application[]>(`${API_URL}/Project/${projectId}`)
        .subscribe((applications: Application[]) => {
            this.setApplications(applications)
        });
  }

  public postApplication(application: Object): Subscription {
    return this.http.post<Application>(API_URL, application)
      .subscribe((application: Application) => {
        this.addApplication(application)
      })
  }

}
