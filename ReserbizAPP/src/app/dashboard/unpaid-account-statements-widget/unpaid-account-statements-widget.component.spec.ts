/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UnpaidAccountStatementsWidgetComponent } from './unpaid-account-statements-widget.component';

describe('UnpaidAccountStatementsWidgetComponent', () => {
  let component: UnpaidAccountStatementsWidgetComponent;
  let fixture: ComponentFixture<UnpaidAccountStatementsWidgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnpaidAccountStatementsWidgetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnpaidAccountStatementsWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
