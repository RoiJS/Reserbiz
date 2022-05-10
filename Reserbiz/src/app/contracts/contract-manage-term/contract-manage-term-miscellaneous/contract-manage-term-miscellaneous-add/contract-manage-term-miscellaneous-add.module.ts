import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { ContractManageTermMiscellaneousAddComponent } from './contract-manage-term-miscellaneous-add.component';
import { SharedModule } from '../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractManageTermMiscellaneousAddComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [ContractManageTermMiscellaneousAddComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractManageTermMiscellaneousAddModule {}
