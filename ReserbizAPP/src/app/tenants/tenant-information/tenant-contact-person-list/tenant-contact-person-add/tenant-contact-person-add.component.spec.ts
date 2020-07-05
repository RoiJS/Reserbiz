/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TenantContactPersonAddComponent } from './tenant-contact-person-add.component';

describe('TenantContactPersonAddComponent', () => {
  let component: TenantContactPersonAddComponent;
  let fixture: ComponentFixture<TenantContactPersonAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TenantContactPersonAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenantContactPersonAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
