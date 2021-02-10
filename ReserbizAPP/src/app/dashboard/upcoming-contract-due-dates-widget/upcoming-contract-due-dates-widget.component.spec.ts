/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UpcomingContractDueDatesWidgetComponent } from './upcoming-contract-due-dates-widget.component';

describe('UpcomingContractDueDatesWidgetComponent', () => {
  let component: UpcomingContractDueDatesWidgetComponent;
  let fixture: ComponentFixture<UpcomingContractDueDatesWidgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpcomingContractDueDatesWidgetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpcomingContractDueDatesWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
