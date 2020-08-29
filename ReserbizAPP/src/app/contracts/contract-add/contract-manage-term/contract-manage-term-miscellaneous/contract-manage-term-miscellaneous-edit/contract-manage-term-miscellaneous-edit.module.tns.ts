import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { ContractManageTermMiscellaneousEditComponent } from './contract-manage-term-miscellaneous-edit.component';
import { SharedModule } from '../../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractManageTermMiscellaneousEditComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [ContractManageTermMiscellaneousEditComponent],
})
export class ContractManageTermMiscellaneousEditModule {}
