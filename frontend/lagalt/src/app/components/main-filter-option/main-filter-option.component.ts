import { Component, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-main-filter-option',
  templateUrl: './main-filter-option.component.html',
  styleUrls: ['./main-filter-option.component.css']
})
export class MainFilterOptionComponent implements OnInit {
  @Input() optionName: string = "";
  @Output() active: boolean = false; // should send to parent that this filter option is active
  constructor() { }

  ngOnInit(): void {
  }

}
