import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptUIListViewModule } from 'nativescript-ui-listview/angular';

import { SharedModule } from '../shared/shared.module';

import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantRoutingModule } from './tenant-routing.module';
import { TenantAddDetailsComponent } from './tenant-add-details/tenant-add-details.component';

@NgModule({
  imports: [NativeScriptUIListViewModule, SharedModule, TenantRoutingModule],
  declarations: [TenantListComponent, TenantAddDetailsComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantsModule {}
