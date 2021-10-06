import { Component, Input, OnInit } from '@angular/core';
import { Project } from '../main-list-of-projects/main-list-of-projects.component';

@Component({
  selector: 'app-main-list-of-projects-item',
  templateUrl: './main-list-of-projects-item.component.html',
  styleUrls: ['./main-list-of-projects-item.component.css']
})
export class MainListOfProjectsItemComponent implements OnInit {
  @Input() project: Project = {
    id: 0,
    name: '',
    type: '',
    thumbnail: 'white'
  };
  constructor() { }

  ngOnInit(): void {
  }

}
