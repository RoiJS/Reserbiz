import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../../shared/shared.module';

import { ContractEditDetailsComponent } from './contract-edit-details.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractEditDetailsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [ContractEditDetailsComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractEditDetailsModule {}
