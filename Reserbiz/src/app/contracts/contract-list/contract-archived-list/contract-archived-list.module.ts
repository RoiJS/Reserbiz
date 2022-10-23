import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { ContractArchivedListComponent } from './contract-archived-list.component';
import { ContractArchivedFilterDialogComponent } from './contract-archived-filter-dialog/contract-archived-filter-dialog.component';

import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractArchivedListComponent,
      },
    ]),
    SharedModule,
  ],
  entryComponents: [ContractArchivedFilterDialogComponent],
  declarations: [
    ContractArchivedListComponent,
    ContractArchivedFilterDialogComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ContractArchivedListModule {}
