import { Component, OnInit } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { ModalDialogParams } from 'nativescript-angular';

import { BaseFormHelper } from '@src/app/_helpers/base-form.helper';

import { AccountStatementFilterFormSource } from '@src/app/_models/account-statement-filter-form.model';
import { AccountStatementFilter } from '@src/app/_models/account-statement-filter.model';

import { PaymentStatusEnum } from '@src/app/_enum/payment-status.enum';
import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';

import { SortOrderValueProvider } from '@src/app/_helpers/sort-order-value-provider.helper';
import { PaymentStatusValueProvider } from '@src/app/_helpers/payment-status-value-provider.helper';

@Component({
  selector: 'ns-contract-account-statement-filter-dialog',
  templateUrl: './contract-account-statement-filter-dialog.component.html',
  styleUrls: ['./contract-account-statement-filter-dialog.component.scss'],
})
export class ContractAccountStatementFilterDialogComponent
  extends BaseFormHelper<AccountStatementFilterFormSource>
  implements OnInit {
  private _accountStatementFilterData: AccountStatementFilter;
  private _accountStatementFilterFormSource: AccountStatementFilterFormSource;
  private _accountStatementFilterFormSourceOriginal: AccountStatementFilterFormSource;

  private _sortOrderValueProvider: SortOrderValueProvider;
  private _paymentStatusValueProvider: PaymentStatusValueProvider;

  constructor(
    private params: ModalDialogParams,
    private translateService: TranslateService
  ) {
    super();
    this._accountStatementFilterData = <AccountStatementFilter>(
      params.context.accountStatementFilter
    );
    this._accountStatementFilterFormSource = new AccountStatementFilterFormSource(
      this._accountStatementFilterData.fromDate,
      this._accountStatementFilterData.toDate,
      this._accountStatementFilterData.paymentStatus,
      this._accountStatementFilterData.sortOrder
    );

    this._accountStatementFilterFormSourceOriginal = this._accountStatementFilterFormSource.clone();
  }

  ngOnInit() {
    this._sortOrderValueProvider = new SortOrderValueProvider(
      this.translateService
    );

    this._paymentStatusValueProvider = new PaymentStatusValueProvider(
      this.translateService
    );
  }

  initFilterOptions(accountStatementFilter: AccountStatementFilter) {
    if (accountStatementFilter.fromDate) {
      this._accountStatementFilterData.fromDate = new Date(
        accountStatementFilter.fromDate
      );
    }

    if (accountStatementFilter.toDate) {
      this._accountStatementFilterData.toDate = new Date(
        accountStatementFilter.toDate
      );
    }

    if (accountStatementFilter.paymentStatus) {
      this._accountStatementFilterData.paymentStatus =
        accountStatementFilter.paymentStatus;
    }

    if (accountStatementFilter.sortOrder) {
      this._accountStatementFilterData.sortOrder =
        accountStatementFilter.sortOrder;
    }
  }

  onConfirm() {
    const filterHasChanged = !this._accountStatementFilterFormSource.isSame(
      this._accountStatementFilterFormSourceOriginal
    );
    if (filterHasChanged) {
      this.setFilterValues();
    }
    this.params.closeCallback({
      filterHasChanged: filterHasChanged,
      filter: this._accountStatementFilterData,
    });
  }

  onReset() {
    this.resetFilterValues();
  }

  setFilterValues() {
    this._accountStatementFilterData.fromDate = this._accountStatementFilterFormSource.fromDate;
    this._accountStatementFilterData.toDate = this._accountStatementFilterFormSource.toDate;
    this._accountStatementFilterData.paymentStatus = this._accountStatementFilterFormSource.paymentStatus;
    this._accountStatementFilterData.sortOrder = this._accountStatementFilterFormSource.sortOrder;
  }

  resetFilterValues() {
    this._accountStatementFilterFormSource = this.reloadFormSource(
      this._accountStatementFilterFormSource,
      <AccountStatementFilter>{
        fromDate: null,
        toDate: null,
        paymentStatus: PaymentStatusEnum.All,
        sortOrder: SortOrderEnum.Ascending,
      }
    );
  }

  closeDialog() {
    this.params.closeCallback();
  }

  get contractFilterFormSource() {
    return this._accountStatementFilterFormSource;
  }

  get sortOrderOptions(): Array<{ key: SortOrderEnum; label: string }> {
    return this._sortOrderValueProvider.sortOrderOptions;
  }

  get paymentStatusOptions(): Array<{ key: PaymentStatusEnum; label: string }> {
    return this._paymentStatusValueProvider.paymentStatusOptions;
  }
}
