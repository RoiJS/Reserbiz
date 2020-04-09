import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { TenantDetailsTabComponent } from './tenant-details-tab.component';
import { TenantInformationComponent } from './tenant-information/tenant-information.component';
import { TenantContractsListComponent } from './tenant-contracts-list/tenant-contracts-list.component';

const routes: Routes = [
  {
    path: 'details-tabs',
    component: TenantDetailsTabComponent,
    children: [
      {
        path: 'tenantDetails/:tenantId',
        component: TenantInformationComponent,
        outlet: 'tenantDetails',
      },
      {
        path: 'tenantContractList',
        component: TenantContractsListComponent,
        outlet: 'tenantContractList',
      },
    ],
  },
  {
    path: '',
    redirectTo: '/tenants/:tenantId/details-tabs',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [NativeScriptRouterModule.forChild(routes)],
  exports: [NativeScriptRouterModule],
})
export class TenantDetailsRoutingModule {}
