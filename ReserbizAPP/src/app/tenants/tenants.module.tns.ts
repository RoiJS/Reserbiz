import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { TenantListComponent } from './tenant-list/tenant-list.component';

import { SharedModule } from '../shared/shared.module';
import { TenantRoutingModule } from './tenant-routing.module.tns';

@NgModule({
  imports: [SharedModule, TenantRoutingModule],
  declarations: [TenantListComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class TenantsModule {}
