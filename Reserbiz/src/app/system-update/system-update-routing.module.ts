import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SystemUpdateComponent } from './system-update.component';

@NgModule({
  imports: [
    RouterModule.forChild([{ path: '', component: SystemUpdateComponent }]),
  ],
  exports: [RouterModule],
  schemas: [NO_ERRORS_SCHEMA],
})
export class SystemUpdateRoutingModule {}
