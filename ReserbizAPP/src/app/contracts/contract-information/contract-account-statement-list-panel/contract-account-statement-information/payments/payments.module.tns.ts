import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { PaymentsComponent } from './payments.component';
import { PaymentDetailsDialogComponent } from './payment-details-dialog/payment-details-dialog.component';

import { SharedModule } from '../../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: PaymentsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [PaymentsComponent, PaymentDetailsDialogComponent],
  entryComponents: [PaymentDetailsDialogComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class PaymentsModule {}
