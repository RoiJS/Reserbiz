import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { TenantDetailsRoutingModule } from './tenant-details-tab-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { TenantDetailsTabComponent } from './tenant-details-tab.component';
import { TenantInformationComponent } from './tenant-information/tenant-information.component';
import { TenantContractsListComponent } from './tenant-contracts-list/tenant-contracts-list.component';
import { TenantContactPersonListComponent } from './tenant-information/tenant-contact-person-list/tenant-contact-person-list.component';
import { TenantDetailsPanelComponent } from './tenant-information/tenant-details-panel/tenant-details-panel.component';

@NgModule({
  imports: [
    TenantDetailsRoutingModule,
    SharedModule,
  ],
  declarations: [
    TenantDetailsTabComponent,
    TenantInformationComponent,
    TenantContractsListComponent,
    TenantContactPersonListComponent,
    TenantDetailsPanelComponent
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantDetailsModule {}
