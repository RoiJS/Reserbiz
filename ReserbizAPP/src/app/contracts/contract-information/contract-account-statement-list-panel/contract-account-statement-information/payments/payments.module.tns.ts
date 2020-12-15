import { NgModule } from '@angular/core';

import { NativeScriptRouterModule } from 'nativescript-angular';

import { PaymentsComponent } from './payments.component';

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
  declarations: [PaymentsComponent],
})
export class PaymentsModule {}
