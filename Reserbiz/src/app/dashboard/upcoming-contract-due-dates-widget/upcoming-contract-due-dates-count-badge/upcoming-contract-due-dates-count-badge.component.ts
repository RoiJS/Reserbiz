import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { BaseWidgetComponent } from '../../../shared/component/base-widget.component';
import { UpcomingContractDueDatesWidgetService } from '../../../_services/upcoming-contract-due-dates-widget.service';

@Component({
  selector: 'ns-upcoming-contract-due-dates-count-badge',
  templateUrl: './upcoming-contract-due-dates-count-badge.component.html',
  styleUrls: [
    './upcoming-contract-due-dates-count-badge.component.scss',
    '../../../shared/styles/base-widget.scss',
  ],
})
export class UpcomingContractDueDatesCountBadgeComponent
  extends BaseWidgetComponent
  implements OnInit, OnDestroy {
  private _listItemCountSub: Subscription;

  constructor(
    private upcomingContractDueDateService: UpcomingContractDueDatesWidgetService
  ) {
    super();
  }

  ngOnInit() {
    this._listItemCountSub = this.upcomingContractDueDateService.listItemCount.subscribe(
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
