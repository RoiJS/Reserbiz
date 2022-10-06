import { Routes } from '@angular/router';

import { TenantListComponent } from './tenant-list/tenant-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'tenant-list',
    pathMatch: 'full',
  },
  {
    path: 'tenant-list',
    component: TenantListComponent,
  },
  {
    path: 'add-tenant',
    loadChildren: () =>
      import('./tenant-add-details-tabs/tenant-add-details-tabs.module').then(
        (m) => m.TenantAddDetailsTabsModule
      ),
  },
  {
    path: ':tenantId',
    loadChildren: () =>
      import('./tenant-information/tenant-information.module').then(
        (m) => m.TenantInformationModule
      ),
  },
];
