import { Routes } from '@angular/router';

import { ContractListComponent } from './contract-list/contract-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'contract-list',
    pathMatch: 'full'
  },
  {
      path: 'contract-list',
      component: ContractListComponent
  }
];
