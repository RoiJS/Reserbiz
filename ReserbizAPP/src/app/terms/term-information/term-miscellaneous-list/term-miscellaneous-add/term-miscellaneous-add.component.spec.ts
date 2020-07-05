/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermMiscellaneousAddComponent } from './term-miscellaneous-add.component';

describe('TermMiscellaneousAddComponent', () => {
  let component: TermMiscellaneousAddComponent;
  let fixture: ComponentFixture<TermMiscellaneousAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermMiscellaneousAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermMiscellaneousAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
