import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-filter',
  templateUrl: './main-filter.component.html',
  styleUrls: ['./main-filter.component.css']
})
export class MainFilterComponent implements OnInit {
  public optionList: string[] = [];
  constructor() { }

  ngOnInit(): void {
    this.optionList = ["best", "new", "all"]
  }

}
