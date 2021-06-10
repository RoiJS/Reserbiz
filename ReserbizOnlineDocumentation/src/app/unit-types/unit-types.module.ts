import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';

import { UnitTypesComponent } from './unit-types.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: UnitTypesComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [UnitTypesComponent],
})
export class UnitTypesModule {}
