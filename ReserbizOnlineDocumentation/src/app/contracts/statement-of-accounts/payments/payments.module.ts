import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from 'src/app/shared/shared.module';
import { PaymentsComponent } from './payments.component';

@NgModule({
  imports: [
    RouterModule.forChild([
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
