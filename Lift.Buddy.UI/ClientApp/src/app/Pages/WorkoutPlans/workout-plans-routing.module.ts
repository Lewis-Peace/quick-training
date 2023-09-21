import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { YourWorkoutsPageComponent } from "./Components/your-workouts-page/your-workouts-page.component";
import { CreateUpdateWorkoutplanPageComponent } from "./Components/create-update-workoutplan-page/create-update-workoutplan-page.component";
import { MyWorkoutsComponent } from "./Components/my-workouts/my-workouts.component";

const routes: Routes = [
  {path: 'training', component: YourWorkoutsPageComponent},
  {path: 'add/:workoutId', component: CreateUpdateWorkoutplanPageComponent},
  {path: 'my-workouts', component: MyWorkoutsComponent,}
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WorkoutPlansRoutingModule { }
