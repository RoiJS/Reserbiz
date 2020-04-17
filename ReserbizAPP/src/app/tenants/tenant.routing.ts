import { Routes } from '@angular/router';

import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantDetailsTabComponent } from './tenant-details-tab/tenant-details-tab.component';
import { TenantAddDetailsComponent } from './tenant-add-details/tenant-add-details.component';

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
    path: 'addTenant',
    component: TenantAddDetailsComponent,
  },
  {
    path: ':tenantId',
    loadChildren: () =>
      import('./tenant-details-tab/tenant-details-tab.module').then(
        (m) => m.TenantDetailsModule
      ),
  },
];
