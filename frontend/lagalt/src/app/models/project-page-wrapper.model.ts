import { Project } from "./project.model";
export interface ProjectPageWrapper {
    next : string,
    previous : string,
    results : Project[],
}
