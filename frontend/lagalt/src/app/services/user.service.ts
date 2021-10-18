import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { UserComplete } from "../models/user/user-complete.model";
import { BehaviorSubject, Observable, of, Subscription } from 'rxjs';
import { catchError, finalize, mapTo, retry, switchMap, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { User } from "@auth0/auth0-spa-js";



const API_URL_USERS = `${environment.apiUrl}Users`
const API_URL_SKILLS = `${environment.apiUrl}Skills`
const defaultUser : UserComplete = {
  id: 0,
  username: "default",
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

  public getUserByUsername(username: string): Subscription{
    return this.http.get<UserComplete>(API_URL_USERS +`/username/${username}`)
        .subscribe((user: UserComplete) => {
            this.setUserById(user)
        });
  }

  public userExists(username: string): Observable<any>{
    return this.http.get(API_URL_USERS +`/username/${username}`)
    

    //return this.http.get<UserComplete>(API_URL_USERS +`/username/${username}`,{observe: "response"})
    /*
    .subscribe(data => {
      if(data.status === 200){
        of(true)
      }
      else{
        of(false)
      }
    })
    */
  }

  public postUserByUsername(username : string): Subscription{
    return this.http.post<UserComplete>(API_URL_USERS,{
      username: username,
      description: "",
      image: "",
      portfolio: "",
      skills: [],
      projects: []
    }).subscribe((res: UserComplete) => {
      this.setUserById(res)
    })
  }
  

  // public putDescriptionById(id : number , description : string) : Subscription {
  //   return this.http.put<User>(API_URL_USERS +`/${id}`, description)
  //   .subscribe((user: User) =>{
  //     this.putUserById$(user)
  //   });
  // }

}
