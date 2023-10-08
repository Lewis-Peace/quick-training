/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AthletesWorkoutAssignmentsComponent } from './athletes-workout-assignments.component';

describe('AthletesWorkoutAssignmentsComponent', () => {
  let component: AthletesWorkoutAssignmentsComponent;
  let fixture: ComponentFixture<AthletesWorkoutAssignmentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AthletesWorkoutAssignmentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AthletesWorkoutAssignmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
