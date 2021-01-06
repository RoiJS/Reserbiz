import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { DashboardComponent } from '@src/app/dashboard/dashboard.component';

import { SharedModule } from '../shared/shared.module';
import { DashboardRoutingModule } from '@src/app/dashboard/dashboard-routing.module';

import { ActiveTenantsCountWidgetComponent } from './active-tenants-count-widget/active-tenants-count-widget.component';
import { AvailableSpacesWidgetComponent } from './available-spaces-widget/available-spaces-widget.component';
import { ActiveContractsWidgetComponent } from './active-contracts-widget/active-contracts-widget.component';
import { UpcomingContractDueDatesWidgetComponent } from './upcoming-contract-due-dates-widget/upcoming-contract-due-dates-widget.component';
import { UpcomingContractDueDatesMonthPickerComponent } from './upcoming-contract-due-dates-widget/upcoming-contract-due-dates-month-picker/upcoming-contract-due-dates-month-picker.component';
import { UpcomingContractDueDatesListComponent } from './upcoming-contract-due-dates-widget/upcoming-contract-due-dates-list/upcoming-contract-due-dates-list.component';
import { UpcomingContractDueDatesCountBadgeComponent } from './upcoming-contract-due-dates-widget/upcoming-contract-due-dates-count-badge/upcoming-contract-due-dates-count-badge.component';

@NgModule({
  imports: [SharedModule, DashboardRoutingModule],
  declarations: [
    DashboardComponent,
    ActiveTenantsCountWidgetComponent,
    AvailableSpacesWidgetComponent,
    ActiveContractsWidgetComponent,
    UpcomingContractDueDatesWidgetComponent,
    UpcomingContractDueDatesMonthPickerComponent,
    UpcomingContractDueDatesListComponent,
    UpcomingContractDueDatesCountBadgeComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class DashboardModule {}
