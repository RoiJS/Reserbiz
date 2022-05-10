import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { ContractManageTermMiscellaneousEditComponent } from './contract-manage-term-miscellaneous-edit.component';
import { SharedModule } from '../../../../shared/shared.module';

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
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractManageTermMiscellaneousEditModule {}
