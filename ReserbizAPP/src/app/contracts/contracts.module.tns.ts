import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { ModalDialogService } from '@nativescript/angular';

import { SharedModule } from '../shared/shared.module';
import { ContractRoutingModule } from './contracts-routing.module.tns';
import { ContractListComponent } from './contract-list/contract-list.component';
import { ContractFilterDialogComponent } from './contract-filter-dialog/contract-filter-dialog.component';

@NgModule({
  imports: [SharedModule, ContractRoutingModule],
  declarations: [ContractListComponent, ContractFilterDialogComponent],
  entryComponents: [ContractFilterDialogComponent],
  providers: [ModalDialogService],
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractsModule {}
