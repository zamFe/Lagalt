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
  private _loading: boolean = false;

  public readonly skills$: BehaviorSubject<Skill[]> = new BehaviorSubject<Skill[]>([]);
  constructor(private readonly http: HttpClient) { }


  public setSkills(skills: Skill[]): void {
    this.skills$.next(skills)
  }

  public addSkill(skill: Skill): void {
    const skills = [...this.skills$.getValue(), skill]
    this.setSkills(skills)
}

  // API CRUD calls
  public getSkills(): Subscription {
    this._loading = true;
    return this.http.get<Skill[]>(API_URL)
        .subscribe((skills: Skill[]) => {
          this._loading = false;
            this.setSkills(skills)
        },
        (error: HttpErrorResponse) => {
            //console.log(error.message);
            alert(error.status + " : " + error.statusText)
        });
  }
  
  public postSkill(skill: Object): Subscription {
    this._loading = true;
    return this.http.post<Skill>(API_URL, skill)
      .subscribe((response) => {
        this._loading = false;
        this.addSkill(response)
      },
            (error: HttpErrorResponse) => {
                //console.log(error.message);
                alert(error.status + " : " + error.statusText)
            })
  }
    
}
