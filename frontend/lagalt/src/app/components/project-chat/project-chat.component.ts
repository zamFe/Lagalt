import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Message, MessagePost } from 'src/app/models/message.model';
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
  public messagePost : MessagePost = {
    userId: 0,
    projectId: 0,
    content: '',
    postedTime: ''
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
    this.messageService.postMessage(newMessageForm)
    this.messagePost.userId =
    this.messagePost.projectId
    this.messagePost.postedTime
    this.messagePost.content = newMessageForm.value.message
    // console.log(newMessageForm.value);
    // console.log(newMessageForm.value.message);


  }

}


