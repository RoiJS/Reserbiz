import { Routes } from '@angular/router';

import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantDetailsComponent } from './tenant-details/tenant-details.component';
import { TenantAddDetailsComponent } from './tenant-add-details/tenant-add-details.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'tenant-list',
    pathMatch: 'full'
  },
  {
    path: 'tenant-list',
    component: TenantListComponent
  },
  {
    path: 'addTenant',
    component: TenantAddDetailsComponent
  },
  {
    path: ':tenantId',
    component: TenantDetailsComponent
  }
];
