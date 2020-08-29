import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptUICalendarModule } from 'nativescript-ui-calendar/angular';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../../shared/shared.module';
import { ContractsCalendarViewComponent } from './contracts-calendar-view.component';
import { ContractEventListPanelComponent } from './contract-event-list-panel/contract-event-list-panel.component';

@NgModule({
  imports: [
    NativeScriptUICalendarModule,
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ContractsCalendarViewComponent,
      },
    ]),
    SharedModule,
  ],
  exports: [ContractsCalendarViewComponent, ContractEventListPanelComponent, SharedModule],
  declarations: [
    ContractsCalendarViewComponent,
    ContractEventListPanelComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ContractsCalendarViewModule {}
