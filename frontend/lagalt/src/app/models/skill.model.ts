import { Project } from "./project.model";
import { UserComplete } from "./user/user-complete.model";

export interface PostSkill {
    id: number,
    name: string,
    user: number[],
    project: number[]
}

export interface Skill{
    id: number,
    name: string
}