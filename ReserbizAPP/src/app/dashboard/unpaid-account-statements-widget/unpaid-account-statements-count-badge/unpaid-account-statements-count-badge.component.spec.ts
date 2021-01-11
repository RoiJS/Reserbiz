/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UnpaidAccountStatementsCountBadgeComponent } from './unpaid-account-statements-count-badge.component';

describe('UnpaidAccountStatementsCountBadgeComponent', () => {
  let component: UnpaidAccountStatementsCountBadgeComponent;
  let fixture: ComponentFixture<UnpaidAccountStatementsCountBadgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnpaidAccountStatementsCountBadgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnpaidAccountStatementsCountBadgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
