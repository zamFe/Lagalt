import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Message, MessagePost } from 'src/app/models/message.model';
import { MessageService } from 'src/app/services/message.service';
import { Observable, Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { UserCompact } from 'src/app/models/user/user-compact.model';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { ProjectService } from 'src/app/services/project.service';
import { Project } from 'src/app/models/project.model';
import { getLocaleDateTimeFormat } from '@angular/common';

@Component({
  selector: 'app-project-chat',
  templateUrl: './project-chat.component.html',
  styleUrls: ['./project-chat.component.css'],
})
export class ProjectChatComponent implements OnInit, OnDestroy {
  private messages$: Subscription;
  public messages: Message[] = [];
  public message: Message = {
    id: 0,
    user: { id: 0, username: '', image: '' },
    content: '',
    postedTime: new Date(),
  };

  public messagePost: MessagePost = {
    userId: 0,
    projectId: 0,
    content: '',
    postedTime: new Date(),
  };

  constructor(
    private readonly messageService: MessageService,
    private readonly userService: UserService,
    private readonly projectService: ProjectService
  ) {
    this.messages$ = this.messageService.messages$.subscribe(
      (messages: Message[]) => {
        this.messages = messages;
      }
    );
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.messages$.unsubscribe();
  }

  postMessage(newMessageForm: NgForm) {
    this.userService.user$.subscribe((user: UserComplete) => {
      this.messagePost.userId = user.id;
    });
    this.projectService.project$.subscribe((project: Project) => {
      this.messagePost.projectId = project.id;
    });
    this.messagePost.postedTime = new Date();
    this.messagePost.content = newMessageForm.value.message;

    console.log(this.messagePost);
    this.messageService.postMessage(this.messagePost);
  }
}
