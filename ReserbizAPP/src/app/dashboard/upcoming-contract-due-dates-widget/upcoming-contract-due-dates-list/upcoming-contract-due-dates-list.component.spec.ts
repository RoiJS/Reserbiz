/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UpcomingContractDueDatesListComponent } from './upcoming-contract-due-dates-list.component';

describe('UpcomingContractDueDatesListComponent', () => {
  let component: UpcomingContractDueDatesListComponent;
  let fixture: ComponentFixture<UpcomingContractDueDatesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpcomingContractDueDatesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpcomingContractDueDatesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
