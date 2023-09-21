import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog'

@Injectable()
export class DialogService {

constructor(
  private dialogService: MatDialog
) { }

  public openCenterDialog(template: ComponentType<unknown>, config: MatDialogConfig) {
    config.width = "25%";
    config.autoFocus = false;
    const dialogRef = this.dialogService.open(
      template,
      config
    )
    return dialogRef;
  }

}
