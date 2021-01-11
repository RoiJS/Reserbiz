/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UnpaidAccountStatementsListComponent } from './unpaid-account-statements-list.component';

describe('UnpaidAccountStatementsListComponent', () => {
  let component: UnpaidAccountStatementsListComponent;
  let fixture: ComponentFixture<UnpaidAccountStatementsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnpaidAccountStatementsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnpaidAccountStatementsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
