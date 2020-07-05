/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermMiscellaneousListComponent } from './term-miscellaneous-list.component';

describe('TermMiscellaneousListComponent', () => {
  let component: TermMiscellaneousListComponent;
  let fixture: ComponentFixture<TermMiscellaneousListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermMiscellaneousListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermMiscellaneousListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
