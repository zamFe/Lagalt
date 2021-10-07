import { Component, Input, OnInit } from '@angular/core';
import { Project } from '../main-list-of-projects/main-list-of-projects.component';

let skillsList = [
  "python",
  "drummer",
  "superhero",
]

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
  public skills: string[] = []
  constructor() { }

  ngOnInit(): void {
    this.skills = skillsList
  }


}
