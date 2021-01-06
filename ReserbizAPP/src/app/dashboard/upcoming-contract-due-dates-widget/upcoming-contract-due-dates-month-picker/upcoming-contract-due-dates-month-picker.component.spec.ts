/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UpcomingContractDueDatesMonthPickerComponent } from './upcoming-contract-due-dates-month-picker.component';

describe('UpcomingContractDueDatesMonthPickerComponent', () => {
  let component: UpcomingContractDueDatesMonthPickerComponent;
  let fixture: ComponentFixture<UpcomingContractDueDatesMonthPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpcomingContractDueDatesMonthPickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpcomingContractDueDatesMonthPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
