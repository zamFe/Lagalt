import { Injectable } from '@angular/core';
import { Skill } from 'src/app/models/skill.model'
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';

const API_URL = `${environment.apiUrl}Skills`
@Injectable({
  providedIn: 'root'
})
export class SkillService {

  public readonly skills$: BehaviorSubject<Skill[]> = new BehaviorSubject<Skill[]>([]);
  constructor(private readonly http: HttpClient) { }


  public setSkills(skills: Skill[]): void {
    this.skills$.next(skills)
  }


  // API CRUD calls
  public getSkills(): Subscription {

    //set professions as enum in storage here.
    return this.http.get<Skill[]>(API_URL)
        .subscribe((skills: Skill[]) => {
            this.setSkills(skills)
        });
  }
}
