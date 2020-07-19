/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TenantAddContactPersonAddComponent } from './tenant-add-contact-person-add.component';

describe('TenantAddContactPersonAddComponent', () => {
  let component: TenantAddContactPersonAddComponent;
  let fixture: ComponentFixture<TenantAddContactPersonAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TenantAddContactPersonAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenantAddContactPersonAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
