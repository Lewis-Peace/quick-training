import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageNotFoundComponent } from './page-not-found.component';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    UIElementsModule
  ],
  declarations: [PageNotFoundComponent]
})
export class PageNotFoundModule { }
