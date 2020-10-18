import { NgModule } from '@angular/core';

import { NativeScriptRouterModule } from 'nativescript-angular/router';
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
})
export class ContractAddModule {}
