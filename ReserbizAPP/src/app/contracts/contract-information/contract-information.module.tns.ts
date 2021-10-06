import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';

import { AuthGuard } from '@src/app/_guards/auth.guard';

import { AddContractAccountStatementDialogComponent } from './add-contract-account-statement-dialog/add-contract-account-statement-dialog.component';
import { ContractInformationComponent } from './contract-information.component';
import { ContractDetailsPanelComponent } from './contract-details-panel/contract-details-panel.component';
import { ContractAccountStatementListPanelComponent } from './contract-account-statement-list-panel/contract-account-statement-list-panel.component';
import { ContractAccountStatementFilterDialogComponent } from './contract-account-statement-filter-dialog/contract-account-statement-filter-dialog.component';
import { AccountStatementTypeDropdownControlComponent } from './add-contract-account-statement-dialog/account-statement-type-dropdown-control/account-statement-type-dropdown-control.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractInformationComponent,
      },
      {
        path: 'edit',
        loadChildren: () =>
          import('./contract-edit-details/contract-edit-details.module').then(
            (m) => m.ContractEditDetailsModule
          ),
      },
      {
        path: 'account-statement/:accountStatementId',
        loadChildren: () =>
          import(
            './contract-account-statement-list-panel/contract-account-statement-information/contract-account-statement-information.module'
          ).then((m) => m.ContractAccountStatementInformationModule),
        canActivate: [AuthGuard],
      },
      {
        path: 'new-account-statement/new-utility-bill-statement-of-account',
        loadChildren: () =>
          import(
            './utility-bill-statement-of-account-form/utility-bill-statement-of-account-form.module'
          ).then((m) => m.UtilityBillStatementOfAccountFormModule),
      },
      {
        path: 'new-account-statement/new-rental-bill-statement-of-account',
        loadChildren: () =>
          import(
            './rental-bill-statement-of-account-form/rental-bill-statement-of-account.module'
          ).then((m) => m.RentalBillStatmentOfAccountModule),
      },
    ]),
    SharedModule,
  ],
  entryComponents: [
    ContractAccountStatementFilterDialogComponent,
    AddContractAccountStatementDialogComponent,
  ],
  declarations: [
    ContractInformationComponent,
    ContractDetailsPanelComponent,
    ContractAccountStatementListPanelComponent,
    ContractAccountStatementFilterDialogComponent,
    AddContractAccountStatementDialogComponent,
    AccountStatementTypeDropdownControlComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ContractInformationModule {}
