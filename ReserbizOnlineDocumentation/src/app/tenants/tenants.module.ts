import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { TenantsComponent } from './tenants.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: TenantsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TenantsComponent],
})
export class TenantsModule {}
