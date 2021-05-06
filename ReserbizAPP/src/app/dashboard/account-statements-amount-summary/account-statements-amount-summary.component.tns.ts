import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { ObservableArray } from '@nativescript/core';

import { TranslateService } from '@ngx-translate/core';

import { Subscription } from 'rxjs';
import { delay } from 'rxjs/operators';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';
import { AccountStatementsAmountSummary } from '@src/app/_models/account-statement-amount-summary.model';
import { AccountStatementSummaryChart } from '@src/app/_models/account-statement-summary-chart.model';

import { AccountStatementService } from '@src/app/_services/account-statement.service';
import { PaymentsService } from '@src/app/_services/payments.service';

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
  implements OnInit, OnDestroy {
  private _accountStatementAmountSummary: ObservableArray<AccountStatementSummaryChart>;
  private _loadListFlagSub: Subscription;

  constructor(
    private accountStatementService: AccountStatementService,
    private translateService: TranslateService,
    private paymentService: PaymentsService,
    private ngZone: NgZone
  ) {
    super();
  }

  ngOnInit() {
    this._loadListFlagSub = this.paymentService.loadPaymentListFlag
      .pipe(delay(2000))
      .subscribe(() => {
        (async () => {
          this.ngZone.run(async () => {
            this._isBusy = true;

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
          });
        })();
      });
  }

  ngOnDestroy() {
    if (this._loadListFlagSub) {
      this._loadListFlagSub.unsubscribe();
    }
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
      accountStatementAmountSummary.totalExpectedAmount;
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
