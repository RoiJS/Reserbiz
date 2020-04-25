import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { TenantDetailsTabComponent } from './tenant-details-tab.component';

const routes: Routes = [
  {
    path: 'details-tabs',
    component: TenantDetailsTabComponent,
    children: [
      {
        path: 'contractList/:tenantId',
        loadChildren: () =>
          import('./tenant-contracts-list/tenant-contracts-list.module').then(
            (m) => m.TenantContractListModule
          ),
        outlet: 'contractList',
      },
    ],
  },
  {
    path: 'edit',
    loadChildren: () =>
      import('./tenant-edit-details/tenant-edit-details.module').then(
        (m) => m.TenantEditDetailsModule
      ),
  },
  {
    path: 'contact-person-list',
    loadChildren: () =>
      import(
        './tenant-contact-person-list/tenant-contact-person-list.module'
      ).then((m) => m.TenantContactPersonListModule),
  },
];

@NgModule({
  imports: [NativeScriptRouterModule.forChild(routes)],
  exports: [NativeScriptRouterModule],
})
export class TenantDetailsRoutingModule {}
