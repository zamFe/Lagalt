import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message, MessageResponse } from 'src/app/models/message.model';

const API_URL = `${environment.apiUrl}Messages`;

@Injectable({
  providedIn: 'root'
})
export class MessageService {

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
    return this.http.get<MessageResponse>(`${API_URL}/Project/${projectId}`)
        .subscribe((messages: MessageResponse) => {
            this.setMessages(messages.results)
        });
  }

  public postMessage(message: Object): Subscription {
    return this.http.post<Message>(API_URL, message)
      .subscribe((message: Message) => {
        this.addMessage(message)
      })
  }
}
