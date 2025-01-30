import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { YourWorkoutsPageComponent } from "./Pages/your-workouts-page/your-workouts-page.component";
import { CreateUpdateWorkoutplanPageComponent } from "./Pages/create-update-workoutplan-page/create-update-workoutplan-page.component";
import { MyWorkoutsComponent } from "./Pages/my-workouts/my-workouts.component";
import { AthletesWorkoutAssignmentsComponent } from "./Pages/my-workouts/Components/athletes-workout-assignments/athletes-workout-assignments.component";
import { AssignedToMeComponent } from "./Pages/assigned-to-me/assigned-to-me.component";

const routes: Routes = [
  {path: 'training', component: YourWorkoutsPageComponent},
  {path: 'add/:workoutId', component: CreateUpdateWorkoutplanPageComponent},
  {path: 'assign/:workoutId', component: AthletesWorkoutAssignmentsComponent},
  {path: 'assigned-to-me', component: AssignedToMeComponent},
  {path: 'my-workouts', component: MyWorkoutsComponent,}
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WorkoutPlansRoutingModule { }
