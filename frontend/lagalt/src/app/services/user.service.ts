import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { User } from "../models/profile.model";
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";


const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly _user$: BehaviorSubject<User[]> = new BehaviorSubject<User[]>([]);

  constructor(private readonly http : HttpClient) { }

  // State CRUD functions

  public getUsers$(): Observable<User[]> {
    return this._user$.asObservable()
  }
  public getUserById$(): Observable<User[]> {
    return this._user$.asObservable()
  }
  public putUserById$(): Observable<User[]> {
    return this._user$.asObservable()
  }
  public postNewUser$(): Observable<User[]> {
    return this._user$.asObservable()
  }
  public setUser(user: User[]): void {
    this._user$.next(user)
}
   // API CRUD calls
  public getUser(): Subscription {

    //set professions as enum in storage here.
    return this.http.get<User[]>(`${API_URL}Users`)
        .subscribe((user: User[]) => {
            this.setUser(user)
        });
  }

}
