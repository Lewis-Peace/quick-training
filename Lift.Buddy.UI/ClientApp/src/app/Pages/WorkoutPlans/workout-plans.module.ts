import { CreateUpdateWorkoutplanPageComponent } from './Pages/create-update-workoutplan-page/create-update-workoutplan-page.component';
import { YourWorkoutsPageComponent } from './Pages/your-workouts-page/your-workouts-page.component';
import { MyWorkoutsComponent } from './Pages/my-workouts/my-workouts.component';
import { WorkoutNavigationMenuComponent } from './Components/workout-navigation-menu/workout-navigation-menu.component';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { WorkoutPlansComponent } from './workout-plans.component';
import { DeleteWorkoutPlanConfirmationPopupComponent } from './Pages/my-workouts/Components/delete-workout-plan-confirmation-popup/delete-workout-plan-confirmation-popup.component';
import { TrainingTabComponent } from './Pages/your-workouts-page/Components/training-tab/training-tab.component';
import { ExerciseRowComponent } from './Components/workoutplan-page/Components/exercise-row/exercise-row.component';
import { DailyWorkoutComponent } from './Components/workoutplan-page/Components/daily-workout/daily-workout.component';
import { AssignedToMeComponent } from './Pages/assigned-to-me/assigned-to-me.component';
import { AthletesWorkoutAssignmentsComponent } from './Pages/my-workouts/Components/athletes-workout-assignments/athletes-workout-assignments.component';
import { WorkoutPlansRoutingModule } from './workout-plans-routing.module';

import { MatFormFieldModule } from '@angular/material/form-field';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatOptionModule } from '@angular/material/core';
import { MatTabsModule } from '@angular/material/tabs';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatMenuModule } from '@angular/material/menu';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { WorkoutplanCardComponent } from './Components/workoutplan-card/workoutplan-card.component';
import { WorkoutplanPageComponent } from './Components/workoutplan-page/workoutplan-page.component';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  imports: [
    CommonModule,
    UIElementsModule,
    ReactiveFormsModule,
    FormsModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatOptionModule,
    BrowserModule,
    RouterModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    WorkoutPlansRoutingModule,
    MatAutocompleteModule,
    MatTabsModule,
    MatMenuModule,
    DragDropModule
  ],
  declarations: [
    WorkoutPlansComponent,
    YourWorkoutsPageComponent,
    CreateUpdateWorkoutplanPageComponent,
    MyWorkoutsComponent,
    WorkoutNavigationMenuComponent,
    DeleteWorkoutPlanConfirmationPopupComponent,
    TrainingTabComponent,
    ExerciseRowComponent,
    DailyWorkoutComponent,
    AthletesWorkoutAssignmentsComponent,
    AssignedToMeComponent,
    WorkoutplanCardComponent,
    WorkoutplanPageComponent
  ],
  exports: [

  ]
})
export class WorkoutPlansModule { }
