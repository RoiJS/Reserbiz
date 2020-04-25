import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { TenantDetailsRoutingModule } from './tenant-details-tab-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { TenantDetailsTabComponent } from './tenant-details-tab.component';
import { TenantInformationComponent } from './tenant-information/tenant-information.component';
import { TenantContactPersonListPanelComponent } from './tenant-information/tenant-contact-person-list-panel/tenant-contact-person-list-panel.component';
import { TenantDetailsPanelComponent } from './tenant-information/tenant-details-panel/tenant-details-panel.component';

@NgModule({
  imports: [TenantDetailsRoutingModule, SharedModule],
  declarations: [
    TenantDetailsTabComponent,
    TenantInformationComponent,
    TenantContactPersonListPanelComponent,
    TenantDetailsPanelComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantDetailsModule {}
