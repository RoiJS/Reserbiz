import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OverviewComponent } from './overview.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: OverviewComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [OverviewComponent],
})
export class OverviewModule {}
