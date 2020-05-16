import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { Tenant } from '../_models/tenant.model';
import { GenderEnum } from '../_enum/gender.enum';

@Injectable({ providedIn: 'root' })
export class AddTenantService {
  private _tenantDetails = new BehaviorSubject<Tenant>(null);
  private _tenantSavedDetails = new BehaviorSubject<void>(null);
  private _cancelTenantSavedDetails = new BehaviorSubject<void>(null);

  constructor() {
    this._tenantDetails.next(this.defaultTenantDetails());
  }

  defaultTenantDetails() {
    const tenant = new Tenant();

    tenant.id = 0;
    tenant.firstName = '';
    tenant.middleName = '';
    tenant.lastName = '';
    tenant.gender = GenderEnum.Male;
    tenant.address = '';
    tenant.contactNumber = '';
    tenant.emailAddress = '';
    tenant.photoUrl = '';

    return tenant;
  }

  resetTenantDetails() {
    this._tenantDetails.next(this.defaultTenantDetails());
  }

  get tenantDetails(): BehaviorSubject<Tenant> {
    return this._tenantDetails;
  }

  get tenantSavedDetails(): BehaviorSubject<void> {
    return this._tenantSavedDetails;
  }

  get cancelTenantSavedDetails(): BehaviorSubject<void> {
    return this._cancelTenantSavedDetails;
  }
}
