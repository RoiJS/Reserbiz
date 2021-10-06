import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { NotificationsComponent } from './notifications.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: NotificationsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [NotificationsComponent],
})
export class NotificationsModule {}
