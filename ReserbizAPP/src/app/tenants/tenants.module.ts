import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { TenantRoutingModule } from './tenant-routing.module';
import { TenantListComponent } from './tenant-list/tenant-list.component';

@NgModule({
  imports: [SharedModule, TenantRoutingModule],
  declarations: [TenantListComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class TenantsModule {}
