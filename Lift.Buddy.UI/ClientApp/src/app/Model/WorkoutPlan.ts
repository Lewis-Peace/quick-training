import { WorkoutDay } from "./WorkoutDay";

export class WorkoutPlan {
  public id: number | undefined;
  public name: string = '';
  public createdBy: string = '';
  public workoutDays: WorkoutDay[] = [];
  public reviewAverage: number = 0;
}
