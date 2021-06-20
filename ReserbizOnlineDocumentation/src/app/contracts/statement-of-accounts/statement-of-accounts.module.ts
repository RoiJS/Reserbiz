import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from 'src/app/shared/shared.module';
import { StatementOfAccountsComponent } from './statement-of-accounts.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: '', component: StatementOfAccountsComponent },
      {
        path: 'payments',
        loadChildren: () =>
          import('./payments/payments.module').then((m) => m.PaymentsModule),
      },
      {
        path: 'penalties',
        loadChildren: () =>
          import('./penalties/penalties.module').then((m) => m.PenaltiesModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [StatementOfAccountsComponent],
})
export class StatementOfAccountsModule {}
