import { UserComplete } from "./user/user-complete.model";

export interface Application {
    id: number,
    user: UserComplete,
    accepted: boolean,
    //seen: boolean,
    motivation: string
}