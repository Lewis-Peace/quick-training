/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SettingToggleRowComponent } from './setting-toggle-row.component';

describe('SettingToggleRowComponent', () => {
  let component: SettingToggleRowComponent;
  let fixture: ComponentFixture<SettingToggleRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SettingToggleRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingToggleRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
