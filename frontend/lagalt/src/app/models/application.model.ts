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
      skills : [],
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
