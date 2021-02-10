/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AccountStatementsAmountSummaryComponent } from './account-statements-amount-summary.component';

describe('AccountStatementsAmountSummaryComponent', () => {
  let component: AccountStatementsAmountSummaryComponent;
  let fixture: ComponentFixture<AccountStatementsAmountSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountStatementsAmountSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountStatementsAmountSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
