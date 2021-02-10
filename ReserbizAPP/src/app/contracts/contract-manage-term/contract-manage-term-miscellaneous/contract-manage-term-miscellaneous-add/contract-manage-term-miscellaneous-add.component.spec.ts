/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ContractManageTermMiscellaneousAddComponent } from './contract-manage-term-miscellaneous-add.component';

describe('ContractManageTermMiscellaneousAddComponent', () => {
  let component: ContractManageTermMiscellaneousAddComponent;
  let fixture: ComponentFixture<ContractManageTermMiscellaneousAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractManageTermMiscellaneousAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractManageTermMiscellaneousAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
