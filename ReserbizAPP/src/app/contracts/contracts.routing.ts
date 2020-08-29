import { Routes } from '@angular/router';

import { ContractListComponent } from './contract-list/contract-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'contract-list',
    pathMatch: 'full',
  },
  {
    path: 'contract-list',
    component: ContractListComponent,
  },
  {
    path: 'contract-calendar',
    loadChildren: () =>
      import(
        './contract-list/contracts-calendar-view/contracts-calendar-view.module'
      ).then((m) => m.ContractsCalendarViewModule),
  },
  {
    path: 'contract-archived-list',
    loadChildren: () =>
      import(
        './contract-list/contract-archived-list/contract-archived-list.module'
      ).then((m) => m.ContractArchivedListModule),
  },
  {
    path: 'contract-add',
    loadChildren: () =>
      import('./contract-add/contract-add.module').then(
        (m) => m.ContractAddModule
      ),
  },
];
