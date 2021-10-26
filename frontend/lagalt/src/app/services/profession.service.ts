import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Profession } from '../models/profession.model';
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';

const API_URL = `${environment.apiUrl}Professions`;

@Injectable({
  providedIn: 'root'
})
export class ProfessionService {
  private _loading: boolean = false;

  public readonly professions$: BehaviorSubject<Profession[]> = new BehaviorSubject<Profession[]>([]);

  constructor(private readonly http: HttpClient) { 
  }

  public setProfessions(professions: Profession[]): void {
    this.professions$.next(professions)
  }

  public getProfessions(): Subscription {
    this._loading = true;
    return this.http.get<Profession[]>(API_URL)
        .subscribe((professions: Profession[]) => {
          this._loading = false;
            this.setProfessions(professions);
        },
        (error: HttpErrorResponse) => {
            //console.log(error.message);
            alert(error.status + " : " + error.statusText)
        });
}
}
