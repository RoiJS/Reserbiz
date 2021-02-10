import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

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
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractArchivedListModule {}
