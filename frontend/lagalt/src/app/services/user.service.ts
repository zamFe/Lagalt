import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { UserComplete } from "../models/user/user-complete.model";
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { finalize, map, retry, switchMap, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";


const API_URL_USERS = `${environment.apiUrl}Users`
const API_URL_SKILLS = `${environment.apiUrl}Skills`
const defaultUser : UserComplete = {
  id: 0,
  userName: "",
  description: "",
  image: "",
  portfolio: "",
  skills: [],
  projects: []
}
@Injectable({
  providedIn: 'root'
})
export class UserService {

  public readonly users$: BehaviorSubject<UserComplete[]> = new BehaviorSubject<UserComplete[]>([]);
  public readonly user$: BehaviorSubject<UserComplete> = new BehaviorSubject<UserComplete>(defaultUser);
  
  constructor(private readonly http : HttpClient) { }

  // State CRUD functions
  public setUsers(users: UserComplete[]): void {
    this.users$.next(users)
  }
  public setUserById(user: UserComplete): void {
    this.user$.next(user)
  }

   // API CRUD calls
  public getUsers(): Subscription {
    //set users as enum in storage here.
    return this.http.get<UserComplete[]>(API_URL_USERS)
        .subscribe((users: UserComplete[]) => {
            this.setUsers(users)
        });
  }

  public getUserById(id : number): Subscription {
    //set users as enum in storage here.
    return this.http.get<UserComplete>(API_URL_USERS +`/${id}`)
        .subscribe((user: UserComplete) => {
            this.setUserById(user)
        });
  }

  // public putDescriptionById(id : number , description : string) : Subscription {
  //   return this.http.put<User>(API_URL_USERS +`/${id}`, description)
  //   .subscribe((user: User) =>{
  //     this.putUserById$(user)
  //   });
  // }

}
