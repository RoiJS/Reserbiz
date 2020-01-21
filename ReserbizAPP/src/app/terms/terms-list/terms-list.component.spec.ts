/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermsListComponent } from './terms-list.component';

describe('TermsListComponent', () => {
  let component: TermsListComponent;
  let fixture: ComponentFixture<TermsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
