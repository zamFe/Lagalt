import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  public userId: number = 0;
  constructor(private readonly userService: UserService) {
    this.userService.user$.subscribe((data) => {
      if (this.userId === 0) {
        this.userId = data.id;
      }
    });
  }

  ngOnInit(): void {
    this.userId = Number(localStorage.getItem('userId'));
  }
}
