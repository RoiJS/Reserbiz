import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { TermsComponent } from './terms.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: TermsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TermsComponent],
})
export class TermsModule {}
