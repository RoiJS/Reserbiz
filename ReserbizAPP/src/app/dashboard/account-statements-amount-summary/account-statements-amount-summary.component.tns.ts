import { Component, OnInit } from '@angular/core';
import { ObservableArray } from '@nativescript/core';

import { TranslateService } from '@ngx-translate/core';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';
import { AccountStatementsAmountSummary } from '@src/app/_models/account-statement-amount-summary.model';
import { AccountStatementSummaryChart } from '@src/app/_models/account-statement-summary-chart.model';

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
  private _accountStatementAmountSummary: ObservableArray<AccountStatementSummaryChart>;

  constructor(
    private accountStatementService: AccountStatementService,
    private translateService: TranslateService
  ) {
    super();
  }

  ngOnInit() {
    this._isBusy = true;
    setTimeout(() => {
      (async () => {
        const accountStatementAmountSummary = await this.accountStatementService.getAccountStatementsAmountSummary();
        const accountStatementAmountSummaryArray: AccountStatementSummaryChart[] = [];

        const totalAmountPaidSummaryChartValue = this.getTotalAmountPaidChartValue(
          accountStatementAmountSummary
        );
        const totalExpectedAmountSummaryChartValue = this.getTotalUnpaidAmountPaidChartValue(
          accountStatementAmountSummary
        );

        accountStatementAmountSummaryArray.push(
          totalAmountPaidSummaryChartValue
        );
        accountStatementAmountSummaryArray.push(
          totalExpectedAmountSummaryChartValue
        );

        this._accountStatementAmountSummary = new ObservableArray<AccountStatementSummaryChart>(
          accountStatementAmountSummaryArray
        );

        this._isBusy = false;
      })();
    }, 2000);
  }

  private getTotalAmountPaidChartValue(
    accountStatementAmountSummary: AccountStatementsAmountSummary
  ): AccountStatementSummaryChart {
    const totalAmountPaidSummary = new AccountStatementSummaryChart();
    totalAmountPaidSummary.value =
      accountStatementAmountSummary.totalAmountPaid;
    const totalAmountPaidLegendLabel = `${this.translateService.instant(
      'DASHBOARD.BODY_SECTION.ACCOUNT_STATEMENTS_AMOUNT_SUMMARY_WIDGET.CHART_LEGENDS.TOTAL_PAID_AMOUNT'
    )} - ${this.translateService.instant(
      'GENERAL_TEXTS.CURRENCY.PHP'
    )} ${totalAmountPaidSummary.formattedValue()}`;
    totalAmountPaidSummary.name = totalAmountPaidLegendLabel;

    return totalAmountPaidSummary;
  }

  private getTotalUnpaidAmountPaidChartValue(
    accountStatementAmountSummary: AccountStatementsAmountSummary
  ): AccountStatementSummaryChart {
    const totalExpectedAmountSummary = new AccountStatementSummaryChart();
    totalExpectedAmountSummary.value =
      accountStatementAmountSummary.totalExpectedAmount -
      accountStatementAmountSummary.totalAmountPaid;
    const totalExpectedAmountLegendLabel = `${this.translateService.instant(
      'DASHBOARD.BODY_SECTION.ACCOUNT_STATEMENTS_AMOUNT_SUMMARY_WIDGET.CHART_LEGENDS.TOTAL_UNPAID_AMOUNT'
    )} - ${this.translateService.instant(
      'GENERAL_TEXTS.CURRENCY.PHP'
    )} ${totalExpectedAmountSummary.formattedValue()}`;
    totalExpectedAmountSummary.name = totalExpectedAmountLegendLabel;

    return totalExpectedAmountSummary;
  }

  get accountStatementAmountSummary(): ObservableArray<AccountStatementSummaryChart> {
    return this._accountStatementAmountSummary;
  }
}
