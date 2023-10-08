import { Component, OnInit } from '@angular/core';
import { Response } from 'src/app/Model/Response';
import { User } from 'src/app/Model/User';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { TrainerService } from 'src/app/Services/trainer.service';

@Component({
  selector: 'app-my-athletes-main',
  templateUrl: './my-athletes-main.component.html',
  styleUrls: ['./my-athletes-main.component.css']
})
export class MyAthletesMainComponent implements OnInit {

  public athletes: User[] = []

  constructor(
    private trainerService: TrainerService,
    private snackbarService: SnackBarService
  ) { }

  ngOnInit() {
    this.initLoadAthletes()
  }

  private async initLoadAthletes() {
    const response = await this.trainerService.getMyAthletes();
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load athletes. Ex: ${response.notes}`);
      return;
    }
    this.athletes = response.body
  }

  public async removeFollower(athlete: User) {
    const response = await this.trainerService.removeAthleteSubscription(athlete);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to remove athlete. Ex: ${response.notes}`);
      return;
    }
    const athleteIdx = this.athletes.indexOf(athlete);
    this.athletes.splice(athleteIdx, 1);
  }

}
