// import User model

export interface Project {
  id: number,
  profession: number,
  title: string,
  image: string,
  skills: number[]
}

export interface ProjectDetailed {
  project: Project,
  messages: string[],
  users: object[],      // change to User[] when model is made
  description: string,
  progress: string,
  source: string
}
