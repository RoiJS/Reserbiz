import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import {
  NativeScriptFormsModule,
  NativeScriptRouterModule,
} from '@nativescript/angular';

import { ChangePasswordComponent } from './change-password.component';
import { SharedModule } from '@src/app/shared/shared.module';

@NgModule({
  imports: [
    NativeScriptFormsModule,
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ChangePasswordComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [ChangePasswordComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ChangePasswordModule {}
