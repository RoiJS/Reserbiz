import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { DashboardComponent } from '@src/app/dashboard/dashboard.component';

import { SharedModule } from '../shared/shared.module';
import { DashboardRoutingModule } from '@src/app/dashboard/dashboard-routing.module';

import { ActiveTenantsCountWidgetComponent } from './active-tenants-count-widget/active-tenants-count-widget.component';
import { AvailableSpacesWidgetComponent } from './available-spaces-widget/available-spaces-widget.component';
import { ActiveContractsWidgetComponent } from './active-contracts-widget/active-contracts-widget.component';

@NgModule({
  imports: [SharedModule, DashboardRoutingModule],
  declarations: [
    DashboardComponent,
    ActiveTenantsCountWidgetComponent,
    AvailableSpacesWidgetComponent,
    ActiveContractsWidgetComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class DashboardModule {}
