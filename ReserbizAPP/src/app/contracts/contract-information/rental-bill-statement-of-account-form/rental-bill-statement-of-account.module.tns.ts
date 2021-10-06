import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../../shared/shared.module';

import { RentalBillStatementOfAccountFormComponent } from './rental-bill-statement-of-account-form.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: RentalBillStatementOfAccountFormComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [RentalBillStatementOfAccountFormComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class RentalBillStatmentOfAccountModule {}
