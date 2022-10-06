import { Routes } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

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
  {
    path: 'manage-term',
    loadChildren: () =>
      import('./contract-manage-term/contract-manage-term.module').then(
        (m) => m.ContractManageTermModule
      ),
  },
  {
    path: ':contractId',
    loadChildren: () =>
      import('./contract-information/contract-information.module').then(
        (m) => m.ContractInformationModule
      ),
    canLoad: [AuthGuard],
  },
];
