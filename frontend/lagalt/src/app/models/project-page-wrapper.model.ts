import { Project } from './project.model';
export interface ProjectPageWrapper {
  totalEntities: number;
  next: string;
  previous: string;
  results: Project[];
}
