import { Component, OnInit } from '@angular/core';
import { isAndroid, isIOS, Page } from '@nativescript/core';

import { DialogService } from '../_services/dialog.service';

import { TranslateService } from '@ngx-translate/core';

declare var android: any;

@Component({
  selector: 'ns-no-connection',
  templateUrl: './no-connection.component.html',
  styleUrls: ['./no-connection.component.scss'],
})
export class NoConnectionComponent implements OnInit {
  constructor(
    private dialogService: DialogService,
    private page: Page,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.hideActionBar();
    this.dialogService.alert(
      this.translateService.instant('NO_CONNECTION_PAGE.DIALOG.TITLE'),
      this.translateService.instant('NO_CONNECTION_PAGE.DIALOG.MESSAGE'),
      () => {
        if (isAndroid) {
          android.os.Process.killProcess(android.os.Process.myPid());
        }
      }
    );
  }

  hideActionBar() {
    this.page.actionBarHidden = true;
  }
}
