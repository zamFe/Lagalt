 import { IdSkill, Skill } from "./skill.model"
 import { Profession } from "./profession.model"
 import { UserCompact } from "./user/user-compact.model"

 export interface Project {
  id: number,
  profession: Profession,
  title: string,
  image: string,
  skills: Skill[],
  users: UserCompact[],
  description: string,
  progress: string,
  source: string,
  administratorIds : number[],
}

export interface PutProject {
  id: number,
  skills: IdSkill[],
  title: string,
  description: string
  progress: string,
  image: string,
  source: string,
}


