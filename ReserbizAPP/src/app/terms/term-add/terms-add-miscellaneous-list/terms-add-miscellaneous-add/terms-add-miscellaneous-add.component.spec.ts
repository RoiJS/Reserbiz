/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermsAddMiscellaneousAddComponent } from './terms-add-miscellaneous-add.component';

describe('TermsAddMiscellaneousAddComponent', () => {
  let component: TermsAddMiscellaneousAddComponent;
  let fixture: ComponentFixture<TermsAddMiscellaneousAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermsAddMiscellaneousAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermsAddMiscellaneousAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
