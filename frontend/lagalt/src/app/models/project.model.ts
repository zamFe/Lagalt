// import User model

export interface Project {
  id: number,
  profession: number,
  title: string,
  image: string,
  skills: number[]
}

export interface ProjectDetailed {
  id: number,
  profession: number,
  title: string,
  image: string,
  skills: number[],
  messages: string[],
  users: Object[],      // change to User[] when model is made
  description: string,
  progress: string,
  source: string
}
