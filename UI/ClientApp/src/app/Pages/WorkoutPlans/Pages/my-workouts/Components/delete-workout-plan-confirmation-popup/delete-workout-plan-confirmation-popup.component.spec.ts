/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DeleteWorkoutPlanConfirmationPopupComponent } from './delete-workout-plan-confirmation-popup.component';

describe('DeleteWorkoutPlanConfirmationPopupComponent', () => {
  let component: DeleteWorkoutPlanConfirmationPopupComponent;
  let fixture: ComponentFixture<DeleteWorkoutPlanConfirmationPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteWorkoutPlanConfirmationPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteWorkoutPlanConfirmationPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
