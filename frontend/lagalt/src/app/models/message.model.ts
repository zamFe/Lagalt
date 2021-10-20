import { UserCompact } from "./user/user-compact.model";

export interface MessageResponse {
    next : string,
    previous : string,
    results : Message[],
}

export interface Message{
  id: number,
  user: UserCompact,
  content: string,
  postedTime: Date,
}
