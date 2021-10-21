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


  private user$: Subscription;
  public user: UserComplete = {
    id: 0,
    username: '',
    description: '',
    image: '',
    portfolio: '',
    skills: [],
    projects: []
  }
  
  constructor(private readonly userService: UserService) {
    this.user$ = this.userService.user$.subscribe(data => {
      this.user = data
    })
   }

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    this.user$.unsubscribe();
  }


}
