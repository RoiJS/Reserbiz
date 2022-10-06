import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../shared/shared.module';
import { NotificationFilterDialogComponent } from './notification-filter-dialog/notification-filter-dialog.component';
import { NotificationListComponent } from './notification-list/notification-list.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: NotificationListComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [NotificationListComponent, NotificationFilterDialogComponent],
  entryComponents: [NotificationFilterDialogComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class NotificationsModule {}
