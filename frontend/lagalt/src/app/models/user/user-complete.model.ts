import { IdSkill, Skill } from "../skill.model";

export interface UserComplete {
  id : number,
  username: string,
  description : string,
  image: string,
  portfolio : string,
  skills : Skill[],
  projects : [],
  hidden : boolean
}

export interface PostUser {
  skills : IdSkill[]
  username: string,
  description: string,
  image: string,
  portfolio: string,

}

