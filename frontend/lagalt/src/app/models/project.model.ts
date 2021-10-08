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
  messages: number[],
  users: number[],
  description: string,
  progress: string,
  source: string | null
}
