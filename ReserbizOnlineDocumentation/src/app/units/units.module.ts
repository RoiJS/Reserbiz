import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { UnitsComponent } from './units.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: UnitsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [UnitsComponent],
})
export class UnitsModule {}
