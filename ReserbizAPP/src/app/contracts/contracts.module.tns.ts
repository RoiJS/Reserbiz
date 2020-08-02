import { NgModule } from '@angular/core';

import { ModalDialogService } from 'nativescript-angular/modal-dialog';

import { SharedModule } from '../shared/shared.module';
import { ContractRoutingModule } from './contracts-routing.module.tns';
import { ContractListComponent } from './contract-list/contract-list.component';
import { ContractFilterDialogComponent } from './contract-filter-dialog/contract-filter-dialog.component';

@NgModule({
  imports: [SharedModule, ContractRoutingModule],
  declarations: [ContractListComponent, ContractFilterDialogComponent],
  entryComponents: [ContractFilterDialogComponent],
  providers: [ModalDialogService],
})
export class ContractsModule {}
