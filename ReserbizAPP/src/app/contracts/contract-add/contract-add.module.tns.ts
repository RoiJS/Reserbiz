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
      },
      {
        path: 'manage-term/:termId',
        loadChildren: () =>
          import('./contract-manage-term/contract-manage-term.module').then(
            (m) => m.ContractManageTermModule
          ),
      },
    ]),
    SharedModule,
  ],
  declarations: [ContractAddComponent],
})
export class ContractAddModule {}
