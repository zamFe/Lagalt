import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message, MessagePost, MessageResponse } from 'src/app/models/message.model';

const API_URL = `${environment.apiUrl}Messages`;

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private _loading: boolean = false;

  public readonly messages$: BehaviorSubject<Message[]> = new BehaviorSubject<Message[]>([]);

  constructor(private readonly http: HttpClient) { }

  public setMessages(messages: Message[]): void {
    this.messages$.next(messages)
  }

  public addMessage(message: Message): void {
    const messages = [...this.messages$.getValue(), message]
    this.setMessages(messages)
  }

  public getMessagesByProjectId(projectId: number): Subscription {
    this._loading = true;
    return this.http.get<MessageResponse>(`${API_URL}/Project/${projectId}`)
        .subscribe((messages: MessageResponse) => {
          this._loading = false;
            this.setMessages(messages.results)
        },
        (error: HttpErrorResponse) => {
            //console.log(error.message);
            alert(error.status + " : " + error.statusText)
        });
  }

  public postMessage(message: Object): Subscription {
    this._loading = true;
    return this.http.post<Message>(API_URL, message)
      .subscribe((message: Message) => {
        this._loading = false;
        this.addMessage(message)
      },
      (error: HttpErrorResponse) => {
          //console.log(error.message);
          alert(error.status + " : " + error.statusText)
      })
  }
}
