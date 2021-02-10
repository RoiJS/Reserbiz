/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ContractAccountStatementFilterDialogComponent } from './contract-account-statement-filter-dialog.component';

describe('ContractAccountStatementFilterDialogComponent', () => {
  let component: ContractAccountStatementFilterDialogComponent;
  let fixture: ComponentFixture<ContractAccountStatementFilterDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractAccountStatementFilterDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractAccountStatementFilterDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
