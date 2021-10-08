import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-main-login',
  templateUrl: './main-login.component.html',
  styleUrls: ['./main-login.component.css']
})
export class MainLoginComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(loginForm: NgForm): void {
    console.log(loginForm.value)
  }
}
