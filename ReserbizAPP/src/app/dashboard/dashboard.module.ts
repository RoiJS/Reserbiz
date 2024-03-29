import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { DashboardComponent } from '@src/app/dashboard/dashboard.component.tns';

import { SharedModule } from '../shared/shared.module';
import { DashboardRoutingModule } from '@src/app/dashboard/dashboard-routing.module.tns';

@NgModule({
  imports: [SharedModule, DashboardRoutingModule],
  declarations: [DashboardComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class DashboardModule {}
