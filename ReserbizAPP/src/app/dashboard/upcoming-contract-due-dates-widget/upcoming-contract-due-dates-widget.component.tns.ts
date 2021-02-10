import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';
import { delay } from 'rxjs/operators';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';
import { UpcomingContractDueDatesWidgetService } from '@src/app/_services/upcoming-contract-due-dates-widget.service';

@Component({
  selector: 'ns-upcoming-contract-due-dates-widget',
  templateUrl: './upcoming-contract-due-dates-widget.component.html',
  styleUrls: [
    './upcoming-contract-due-dates-widget.component.scss',
    '../../shared/styles/base-widget.scss',
  ],
})
export class UpcomingContractDueDatesWidgetComponent
  extends BaseWidgetComponent
  implements OnInit, OnDestroy {
  private _isBusySub: Subscription;
  constructor(
    private upcomingContractsDueDateService: UpcomingContractDueDatesWidgetService
  ) {
    super();
  }

  ngOnInit() {
    this._isBusySub = this.upcomingContractsDueDateService.isBusy
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
