import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { ContractArchivedListComponent } from './contract-archived-list.component';

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
  declarations: [ContractArchivedListComponent],
})
export class ContractArchivedListModule {}
