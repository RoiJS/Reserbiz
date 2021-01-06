/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UpcomingContractDueDatesCountBadgeComponent } from './upcoming-contract-due-dates-count-badge.component';

describe('UpcomingContractDueDatesCountBadgeComponent', () => {
  let component: UpcomingContractDueDatesCountBadgeComponent;
  let fixture: ComponentFixture<UpcomingContractDueDatesCountBadgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpcomingContractDueDatesCountBadgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpcomingContractDueDatesCountBadgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
