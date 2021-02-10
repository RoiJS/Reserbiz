import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';
import { delay } from 'rxjs/operators';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';

import { BaseWidgetService } from '@src/app/_services/base-widget.service';

@Component({
  selector: 'ns-unpaid-account-statements-widget',
  templateUrl: './unpaid-account-statements-widget.component.html',
  styleUrls: [
    './unpaid-account-statements-widget.component.scss',
    '../../shared/styles/base-widget.scss',
  ],
})
export class UnpaidAccountStatementsWidgetComponent
  extends BaseWidgetComponent
  implements OnInit, OnDestroy {
  private _isBusySub: Subscription;
  constructor(private baseWidgetService: BaseWidgetService) {
    super();
  }

  ngOnInit() {
    this._isBusySub = this.baseWidgetService.isBusy
      .pipe(delay(500))
      .subscribe((isBusy: boolean) => {
        this._isBusy = isBusy;
      });
  }

  ngOnDestroy() {
    if (this._isBusySub) {
      this._isBusySub.unsubscribe();
    }
  }
}
