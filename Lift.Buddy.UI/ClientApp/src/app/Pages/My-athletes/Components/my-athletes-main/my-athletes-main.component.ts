import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { SubscriptionType } from 'src/app/Model/Subscription';
import { User } from 'src/app/Model/User';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { TrainerService } from 'src/app/Services/trainer.service';
import { UserService } from 'src/app/Services/user.service';

export class UserResume {
  public username: string = '';
  public subscription: string | undefined;
  public expiration: Date | undefined;
  public notes: string = '';
}

@Component({
  selector: 'app-my-athletes-main',
  templateUrl: './my-athletes-main.component.html',
  styleUrls: ['./my-athletes-main.component.css']
})
export class MyAthletesMainComponent implements OnInit {

  public pageSizeOptions: number[] = [5, 10, 15];
  public athletes: User[] = []
  public displayedColumns: string[] = ['username', 'subscription', 'expiration', 'notes', 'remove'];
  public tableData: MatTableDataSource<UserResume> = new MatTableDataSource();

  @ViewChild(MatTable) table: MatTable<UserResume> | undefined;
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  
  constructor(
    private userService: UserService,
    private trainerService: TrainerService,
    private snackbarService: SnackBarService
  ) { }

  async ngOnInit() {
    await this.initLoadAthletes();
    await this.initTableData();
  }

  ngAfterViewInit() {
    if (this.tableData && this.paginator) {
      this.tableData.paginator = this.paginator;
    }
  }

  private async initLoadAthletes() {
    const response = await this.trainerService.getMyAthletes();
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load athletes. Ex: ${response.notes}`);
      return;
    }
    this.athletes = response.body;
  }

  private async initTableData() {
    const response = await this.userService.getSubscriptions();
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load subscriptions. Ex: ${response.notes}`);
      return;
    }
    const list = []
    for (let subscriptionIdx = 0; subscriptionIdx < response.body?.length; subscriptionIdx++) {
      const subscription = response.body[subscriptionIdx];
      const athlete = this.athletes.find(x => x.id == subscription.athleteId);
      let userResume = new UserResume();
      userResume.username = athlete!.credentials.username;
      userResume.subscription = SubscriptionType[subscription.subscriptionType];
      userResume.expiration = subscription.expiration;
      list.push(userResume);
    }
    this.tableData.data = list;
  }

  public async removeFollower(athlete: User) {
    const response = await this.trainerService.removeAthleteSubscription(athlete);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to remove athlete. Ex: ${response.notes}`);
      return;
    }
    const data = this.tableData?.data;
    const athleteIdx = data.findIndex(x => x.username == athlete.username);
    this.tableData.data.splice(athleteIdx!, 1);
    this.table?.renderRows();
  }

}
