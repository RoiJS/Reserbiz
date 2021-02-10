import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';
import { SharedModule } from '../../shared/shared.module';

import { ContractAddComponent } from './contract-add.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractAddComponent,
      }
    ]),
    SharedModule,
  ],
  declarations: [ContractAddComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractAddModule {}
