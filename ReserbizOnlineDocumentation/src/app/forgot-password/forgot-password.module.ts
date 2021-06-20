import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ForgotPasswordComponent } from './forgot-password.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: ForgotPasswordComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [ForgotPasswordComponent],
})
export class ForgotPasswordModule {}
