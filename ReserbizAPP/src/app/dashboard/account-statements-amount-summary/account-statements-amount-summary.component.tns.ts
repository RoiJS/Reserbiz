import { Component, OnInit } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';
import { AccountStatementSummaryChart } from '@src/app/_models/account-statemet-summary-chart.model';

import { AccountStatementService } from '@src/app/_services/account-statement.service';

@Component({
  selector: 'ns-account-statements-amount-summary',
  templateUrl: './account-statements-amount-summary.component.html',
  styleUrls: [
    './account-statements-amount-summary.component.scss',
    '../../shared/styles/base-widget.scss',
  ],
})
export class AccountStatementsAmountSummaryComponent
  extends BaseWidgetComponent
  implements OnInit {
  private _accountStatementAmountSummary: AccountStatementSummaryChart[] = [];

  constructor(
    private accountStatementService: AccountStatementService,
    private translateService: TranslateService
  ) {
    super();
  }

  ngOnInit() {
    (async () => {
      this._isBusy = true;
      const accountStatementAmountSummary = await this.accountStatementService.getAccountStatementsAmountSummary();

      this._accountStatementAmountSummary.push({
        name: this.translateService.instant(
          'DASHBOARD.BODY_SECTION.ACCOUNT_STATEMENTS_AMOUNT_SUMMARY_WIDGET.CHART_LEGENDS.TOTAL_PAID_AMOUNT'
        ),
        value: accountStatementAmountSummary.totalAmountPaid,
      });
      this._accountStatementAmountSummary.push({
        name: this.translateService.instant(
          'DASHBOARD.BODY_SECTION.ACCOUNT_STATEMENTS_AMOUNT_SUMMARY_WIDGET.CHART_LEGENDS.TOTAL_EXPECTED_AMOUNT'
        ),
        value: accountStatementAmountSummary.totalExpectedAmount,
      });

      this._isBusy = false;
    })();
  }

  get accountStatementAmountSummary(): AccountStatementSummaryChart[] {
    return this._accountStatementAmountSummary;
  }
}
