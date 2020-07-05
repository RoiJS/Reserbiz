/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermAddComponent } from './term-add.component';

describe('TermAddComponent', () => {
  let component: TermAddComponent;
  let fixture: ComponentFixture<TermAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
