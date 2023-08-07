import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar'

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  constructor(
    private snackBar: MatSnackBar
  ) { }

  public openSnackbar(message: string, action: string | undefined = undefined) {
    this.snackBar.open(message, action, {
      duration: 2000
    });
  }

}
