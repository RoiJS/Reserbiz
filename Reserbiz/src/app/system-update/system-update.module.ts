import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { SystemUpdateComponent } from './system-update.component';
import { SystemUpdateRoutingModule } from './system-update-routing.module'; 

@NgModule({
  imports: [SharedModule, SystemUpdateRoutingModule],
  declarations: [SystemUpdateComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class SystemUpdateModule {}
