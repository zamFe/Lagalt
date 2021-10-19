import { UserCompact } from "./user/user-compact.model";

export interface Message {
    id: number,
    user: UserCompact,
    content: string,
    postedTime: Date
}