import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from 'src/app/shared/shared.module';
import { PenaltiesComponent } from './penalties.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: PenaltiesComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [PenaltiesComponent],
})
export class PenaltiesModule {}
