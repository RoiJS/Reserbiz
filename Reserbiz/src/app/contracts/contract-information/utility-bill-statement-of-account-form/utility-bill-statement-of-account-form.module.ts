import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {
  NativeScriptFormsModule,
  NativeScriptRouterModule,
} from '@nativescript/angular';

import { SharedModule } from '../../../shared/shared.module';

import { UtilityBillStatementOfAccountFormComponent } from './utility-bill-statement-of-account-form.component';

@NgModule({
  imports: [
    NativeScriptFormsModule,
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: UtilityBillStatementOfAccountFormComponent,
      },
    ]),
    SharedModule,
    ReactiveFormsModule,
  ],
  declarations: [UtilityBillStatementOfAccountFormComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class UtilityBillStatementOfAccountFormModule {}
