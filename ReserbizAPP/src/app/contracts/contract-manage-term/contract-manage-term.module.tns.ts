import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';
import { ContractManageTermComponent } from './contract-manage-term.component';
import { ContractManageTermMiscellaneousComponent } from './contract-manage-term-miscellaneous/contract-manage-term-miscellaneous.component';
import { ContractManageTermFormComponent } from './contract-manage-term-form/contract-manage-term-form.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractManageTermComponent,
      },
      {
        path: 'add-miscellaneous',
        loadChildren: () =>
          import(
            './contract-manage-term-miscellaneous/contract-manage-term-miscellaneous-add/contract-manage-term-miscellaneous-add.module'
          ).then((m) => m.ContractManageTermMiscellaneousAddModule),
      },
      {
        path: 'edit-miscellaneous/:termMiscellaneousId',
        loadChildren: () =>
          import(
            './contract-manage-term-miscellaneous/contract-manage-term-miscellaneous-edit/contract-manage-term-miscellaneous-edit.module'
          ).then((m) => m.ContractManageTermMiscellaneousEditModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [
    ContractManageTermComponent,
    ContractManageTermFormComponent,
    ContractManageTermMiscellaneousComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class ContractManageTermModule {}
