import { Component, OnInit } from '@angular/core';

import { RouterExtensions } from '@nativescript/angular';

import { Page } from '@nativescript/core';
import {
  connectionType,
  getConnectionType,
} from '@nativescript/core/connectivity';

import { CheckConnectionService } from '../_services/check-connection.service';

@Component({
  selector: 'ns-no-connection',
  templateUrl: './no-connection.component.html',
  styleUrls: ['./no-connection.component.scss'],
})
export class NoConnectionComponent implements OnInit {
  constructor(
    private checkConnectionService: CheckConnectionService,
    private page: Page,
    private routerExtensions: RouterExtensions
  ) {}

  ngOnInit() {
    this.hideActionBar();
  }

  hideActionBar() {
    this.page.actionBarHidden = true;
  }

  tryAgain() {
    const currentConnectionType = getConnectionType();
    if (currentConnectionType !== connectionType.none) {
      this.routerExtensions.navigate(['/auth']);
    }
  }
}
