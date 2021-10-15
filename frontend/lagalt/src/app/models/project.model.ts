 import { Skill } from "./skill.model"
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
  source: string | null,
  administratorIds : number[],
}


