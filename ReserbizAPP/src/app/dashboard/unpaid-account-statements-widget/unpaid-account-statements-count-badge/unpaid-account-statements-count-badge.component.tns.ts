import { Component, OnDestroy, OnInit } from '@angular/core';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';

import { BaseWidgetService } from '@src/app/_services/base-widget.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'ns-unpaid-account-statements-count-badge',
  templateUrl: './unpaid-account-statements-count-badge.component.html',
  styleUrls: [
    './unpaid-account-statements-count-badge.component.scss',
    '../../../shared/styles/base-widget.scss',
  ],
})
export class UnpaidAccountStatementsCountBadgeComponent
  extends BaseWidgetComponent
  implements OnInit, OnDestroy {
  private _listItemCountSub: Subscription;

  constructor(private baseWidgetService: BaseWidgetService) {
    super();
  }

  ngOnInit() {
    this._listItemCountSub = this.baseWidgetService.listItemCount.subscribe(
      (currentCount: number) => {
        this._entityCount = currentCount;
      }
    );
  }

  ngOnDestroy() {
    if (this._listItemCountSub) {
      this._listItemCountSub.unsubscribe();
    }
  }
}
