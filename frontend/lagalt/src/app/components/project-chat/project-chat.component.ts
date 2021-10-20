import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Message } from 'src/app/models/message.model';
import { MessageService } from 'src/app/services/message.service';
import { Observable, Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { UserCompact } from 'src/app/models/user/user-compact.model'
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-project-chat',
  templateUrl: './project-chat.component.html',
  styleUrls: ['./project-chat.component.css']
})
export class ProjectChatComponent implements OnInit, OnDestroy {

  private messages$ : Subscription;
  public messages : Message[] = []
  public message : Message = {
    id : 0,
    user : {id : 0, username : '', image : ''},
    content : '',
    postedTime : new Date
  }



  constructor(private readonly messageService : MessageService, private readonly userService : UserService) {
    this.messages$ = this.messageService.messages$.subscribe((messages : Message[]) => {
      this.messages = messages;
    })
   }

  ngOnInit(): void {
    this.messageService.getMessagesByProjectId(1)
  }

  ngOnDestroy(): void {
    this.messages$.unsubscribe()
  }
  postMessage(newMessageForm : NgForm){
    // this.messageService.postMessage(newMessageForm)
    //   this.message.content = newMessageForm.value.description
    //   this.message.id = this.userService
    //   this.message.postedTime = this.message.postedTime
    //   this.message.user = this.userService

  }

}


