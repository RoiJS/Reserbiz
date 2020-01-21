import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ContractRoutingModule } from './contracts-routing.module.tns';
import { ContractListComponent } from './contract-list/contract-list.component';

@NgModule({
  imports: [SharedModule, ContractRoutingModule],
  declarations: [ContractListComponent]
})
export class ContractsModule {}
