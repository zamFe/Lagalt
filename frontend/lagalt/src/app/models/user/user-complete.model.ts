import { Skill } from '../skill.model';

export interface UserComplete {
  id: number;
  username: string;
  description: string;
  image: string;
  portfolio: string;
  skills: Skill[];
  projects: [];
  hidden: boolean;
}

export interface PostUser {
  skills: number[];
  username: string;
  description: string;
  image: string;
  portfolio: string;
}

export interface PutUser {
  id: number;
  skills: number[];
  hidden: boolean;
  username: string;
  description: string;
  image: string;
  portfolio: string;
}
