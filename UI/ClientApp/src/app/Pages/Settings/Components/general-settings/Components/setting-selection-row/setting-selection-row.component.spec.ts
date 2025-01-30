/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SettingSelectionRowComponent } from './setting-selection-row.component';

describe('SettingSelectionRowComponent', () => {
  let component: SettingSelectionRowComponent;
  let fixture: ComponentFixture<SettingSelectionRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SettingSelectionRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingSelectionRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
