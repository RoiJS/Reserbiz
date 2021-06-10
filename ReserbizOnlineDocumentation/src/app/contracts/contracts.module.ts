import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { ContractsComponent } from './contracts.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: ContractsComponent,
      },
      {
        path: 'statement-of-accounts',
        loadChildren: () =>
          import('./statement-of-accounts/statement-of-accounts.module').then(
            (m) => m.StatementOfAccountsModule
          ),
      },
    ]),
    SharedModule,
  ],
  declarations: [ContractsComponent],
})
export class ContractsModule {}
