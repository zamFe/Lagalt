import { Component, Input, OnInit } from '@angular/core';

// TEMP MODEL OF PROJECT
interface Project {
  id: number,
  name: string,
  type: string,           // could be enum instead possibly
  thumbnail: string,      // only a background color for now
  //members: string[]
}

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
