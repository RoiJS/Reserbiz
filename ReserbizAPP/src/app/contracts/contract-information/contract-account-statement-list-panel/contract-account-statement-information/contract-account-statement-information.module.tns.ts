import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import {
  NativeScriptFormsModule,
  NativeScriptRouterModule,
} from 'nativescript-angular';

import { ContractAccountStatementInformationComponent } from './contract-account-statement-information.component';
import { SharedModule } from '../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptFormsModule,
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractAccountStatementInformationComponent,
      },
      {
        path: 'payments/:accountStatementId',
        loadChildren: () =>
          import('./payments/payments.module').then((m) => m.PaymentsModule),
      },
      {
        path: 'penalties/:accountStatementId',
        loadChildren: () =>
          import('./penalties/penalties.module').then((m) => m.PenaltiesModule),
      },
    ]),
    ReactiveFormsModule,
    SharedModule,
  ],
  declarations: [ContractAccountStatementInformationComponent],
})
export class ContractAccountStatementInformationModule {}
