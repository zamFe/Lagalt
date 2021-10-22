import { Skill } from "./skill.model";
import { UserComplete } from "./user/user-complete.model";

export interface ApplicationResponse {
  next : string,
  previous : string,
  results : Application[]
}

export interface Application {
    id: number,
    user: {
      id : number,
      skills : Skill[],
      username : string,
      description : string,
      image : string,
      portfolio : string
    },
    accepted: boolean,
    seen: boolean,
    motivation: string
}

export interface PostApplication {
  projectId : number,
  userId : number,
  motivation: string
}
export interface PutApplication {
  id : number,
  accepted : boolean,
  seen: boolean
}
