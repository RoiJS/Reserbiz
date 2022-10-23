import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import {
  NativeScriptFormsModule,
  NativeScriptRouterModule,
} from '@nativescript/angular';

import { ForgotPasswordComponent } from './forgot-password.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptFormsModule,
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ForgotPasswordComponent,
      },
      {
        path: 'change-password',
        loadChildren: () =>
          import('./change-password/change-password.module').then(
            (m) => m.ChangePasswordModule
          ),
      },
    ]),
    SharedModule,
  ],
  declarations: [ForgotPasswordComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ForgotPasswordModule {}
