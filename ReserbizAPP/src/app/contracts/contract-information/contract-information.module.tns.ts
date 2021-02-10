import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';

import { ContractInformationComponent } from './contract-information.component';
import { ContractDetailsPanelComponent } from './contract-details-panel/contract-details-panel.component';
import { ContractAccountStatementListPanelComponent } from './contract-account-statement-list-panel/contract-account-statement-list-panel.component';
import { ContractAccountStatementFilterDialogComponent } from './contract-account-statement-filter-dialog/contract-account-statement-filter-dialog.component';

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
        path: 'account-statement/:accountStatmentId',
        loadChildren: () =>
          import(
            './contract-account-statement-list-panel/contract-account-statement-information/contract-account-statement-information.module'
          ).then((m) => m.ContractAccountStatementInformationModule),
      },
    ]),
    SharedModule,
  ],
  entryComponents: [ContractAccountStatementFilterDialogComponent],
  declarations: [
    ContractInformationComponent,
    ContractDetailsPanelComponent,
    ContractAccountStatementListPanelComponent,
    ContractAccountStatementFilterDialogComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractInformationModule {}
