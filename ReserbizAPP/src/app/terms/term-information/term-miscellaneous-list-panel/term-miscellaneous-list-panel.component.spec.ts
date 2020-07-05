/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TermMiscellaneousListPanelComponent } from './term-miscellaneous-list-panel.component';

describe('TermMiscellaneousListPanelComponent', () => {
  let component: TermMiscellaneousListPanelComponent;
  let fixture: ComponentFixture<TermMiscellaneousListPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermMiscellaneousListPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermMiscellaneousListPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
