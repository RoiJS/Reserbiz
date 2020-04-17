import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptCommonModule } from 'nativescript-angular/common';

import { NativeScriptUIDataFormModule } from 'nativescript-ui-dataform/angular/dataform-directives';

import { TenantDetailsRoutingModule } from './tenant-details-tab-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { TenantDetailsTabComponent } from './tenant-details-tab.component';
import { TenantInformationComponent } from './tenant-information/tenant-information.component';
import { TenantContractsListComponent } from './tenant-contracts-list/tenant-contracts-list.component';

@NgModule({
  imports: [
    TenantDetailsRoutingModule,
    NativeScriptUIDataFormModule,
    NativeScriptCommonModule,
    SharedModule,
  ],
  declarations: [
    TenantDetailsTabComponent,
    TenantInformationComponent,
    TenantContractsListComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantDetailsModule {}
