import { Component, Input, OnInit } from '@angular/core';
import { Project } from "../../models/project.model";

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
    profession: 0,
    title: '',
    image: '',
    skills: [],
    messages: [],
    users: [],
    description: '',
    progress: '',
    source: '',
  };
  public skills: string[] = []
  constructor() { }

  ngOnInit(): void {
    this.skills = skillsList
  }


}
