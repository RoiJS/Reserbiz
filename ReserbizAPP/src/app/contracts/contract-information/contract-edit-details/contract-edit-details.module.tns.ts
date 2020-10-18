import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular';

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
})
export class ContractEditDetailsModule {}
