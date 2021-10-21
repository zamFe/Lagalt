import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserComplete } from 'src/app/models/user/user-complete.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public userId: number = 0;
  constructor() {
   }

  ngOnInit(): void {
    this.userId = Number(localStorage.getItem('userId'))
  }


}
