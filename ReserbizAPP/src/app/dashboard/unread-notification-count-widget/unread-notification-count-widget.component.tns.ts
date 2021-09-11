import { Component, OnInit } from '@angular/core';
import { RouterExtensions } from '@nativescript/angular';

import { ExtendedNavigationExtras } from '@nativescript/angular/router/router-extensions';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';

import { UserNotificationService } from '@src/app/_services/user-notification.service';

@Component({
  selector: 'ns-unread-notification-count-widget',
  templateUrl: './unread-notification-count-widget.component.html',
  styleUrls: ['./unread-notification-count-widget.component.scss'],
})
export class UnreadNotificationCountWidgetComponent
  extends BaseWidgetComponent
  implements OnInit
{
  constructor(
    private userNotificationService: UserNotificationService,
    private router: RouterExtensions
  ) {
    super();
  }

  ngOnInit() {
    this._isBusy = true;
    setTimeout(() => {
      (async () => {
        await this.setUserNotificationUnreadCount();
        this._isBusy = false;
      })();
    }, 2000);
  }

  goToNotificationPage() {
    const routeConfig: ExtendedNavigationExtras = {
      transition: {
        name: 'slideLeft',
      },
    };
    this.router.navigate(['notifications'], routeConfig);
  }

  private async setUserNotificationUnreadCount() {
    this.userNotificationService.unreadNotificationCount.subscribe(
      (count: number) => {
        this._entityCount = count;
      }
    );

    this.userNotificationService.checkNotificationUpdate();
  }
}
