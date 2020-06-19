import { Injectable } from '@angular/core';

import { Tenant } from '../_models/tenant.model';
import { EntityService } from './entity.service';

@Injectable({ providedIn: 'root' })
export class AddTenantService extends EntityService<Tenant> {
  constructor() {
    super(Tenant);
  }
}
