/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ContractManageTermMiscellaneousComponent } from './contract-manage-term-miscellaneous.component';

describe('ContractManageTermMiscellaneousComponent', () => {
  let component: ContractManageTermMiscellaneousComponent;
  let fixture: ComponentFixture<ContractManageTermMiscellaneousComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractManageTermMiscellaneousComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractManageTermMiscellaneousComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
