/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermsAddMiscellaneousEditComponent } from './terms-add-miscellaneous-edit.component';

describe('TermsAddMiscellaneousEditComponent', () => {
  let component: TermsAddMiscellaneousEditComponent;
  let fixture: ComponentFixture<TermsAddMiscellaneousEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermsAddMiscellaneousEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermsAddMiscellaneousEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
