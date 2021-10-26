import { Injectable} from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { PutUser, UserComplete } from "../models/user/user-complete.model";
import { BehaviorSubject, Observable, of, Subscription } from 'rxjs';
import { catchError, finalize, mapTo, retry, switchMap, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";



const API_URL_USERS = `${environment.apiUrl}Users`
const defaultUser : UserComplete = {
  id: 0,
  username: "default",
  description: "",
  image: "",
  portfolio: "",
  skills: [],
  projects: [],
  hidden: false
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
        },
        (error: HttpErrorResponse) => {
            //console.log(error.message);
            alert(error.message)
        });
  }

  public getUserById(id : number): Subscription {
    //set users as enum in storage here.
    return this.http.get<UserComplete>(API_URL_USERS +`/${id}`)
        .subscribe((user: UserComplete) => {
            this.setUserById(user)
        },
        (error: HttpErrorResponse) => {
            //console.log(error.message);
            alert(error.message)
        });
  }

  // trenger ikke error
  public getUserByUsername(username: string): Subscription{
    return this.http.get<UserComplete>(`${API_URL_USERS}/username/${username}`)
        .subscribe((user: UserComplete) => {
            this.setUserById(user)
        });
  }

  // trenger ikke error
  public userExists(username: string): Observable<any>{
    return this.http.get(`${API_URL_USERS}/username/${username}`)
  }


  public postUserByUsername(username : string): Subscription {
    let newUser = {
      username: username,
      skills: [],
      description : "",
      image: "",
      portfolio:""}
    return this.http.post<UserComplete>(API_URL_USERS, newUser)
      .subscribe((response: UserComplete) => {
        console.log(response);

        this.setUserById(response)
    },
    (error: HttpErrorResponse) => {
        //console.log(error.message);
        alert(error.message)
    });
  }

  // IKKE TESTET
  public putUser(user: PutUser): Subscription {
    return this.http.put<UserComplete>(`${API_URL_USERS}/${user.id}`, user)
      .subscribe(() => {
        //this.setUserById(user)
      },
      (error: HttpErrorResponse) => {
          //console.log(error.message);
          alert(error.message)
      });
  }
  public getTest() {
    return this.http.get<UserComplete[]>(API_URL_USERS).subscribe(data => console.log(data))
}
}
