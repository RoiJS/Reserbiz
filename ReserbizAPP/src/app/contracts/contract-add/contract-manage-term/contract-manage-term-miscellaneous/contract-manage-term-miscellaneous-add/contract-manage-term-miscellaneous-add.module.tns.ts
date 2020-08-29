import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { ContractManageTermMiscellaneousAddComponent } from './contract-manage-term-miscellaneous-add.component';
import { SharedModule } from '../../../../../shared/shared.module';

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
})
export class ContractManageTermMiscellaneousAddModule {}
