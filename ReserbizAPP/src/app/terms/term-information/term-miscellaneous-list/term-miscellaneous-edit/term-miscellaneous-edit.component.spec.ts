/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermMiscellaneousEditComponent } from './term-miscellaneous-edit.component';

describe('TermMiscellaneousEditComponent', () => {
  let component: TermMiscellaneousEditComponent;
  let fixture: ComponentFixture<TermMiscellaneousEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermMiscellaneousEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermMiscellaneousEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
