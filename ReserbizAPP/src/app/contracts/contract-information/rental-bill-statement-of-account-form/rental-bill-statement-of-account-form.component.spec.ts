/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RentalBillStatementOfAccountFormComponent } from './rental-bill-statement-of-account-form.component';

describe('RentalBillStatementOfAccountFormComponent', () => {
  let component: RentalBillStatementOfAccountFormComponent;
  let fixture: ComponentFixture<RentalBillStatementOfAccountFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RentalBillStatementOfAccountFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalBillStatementOfAccountFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
