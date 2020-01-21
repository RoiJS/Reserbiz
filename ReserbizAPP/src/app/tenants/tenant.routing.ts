import { Routes } from '@angular/router';

import { TenantListComponent } from './tenant-list/tenant-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'tenant-list',
    pathMatch: 'full'
  },
  {
    path: '',
    component: TenantListComponent
  }
];
