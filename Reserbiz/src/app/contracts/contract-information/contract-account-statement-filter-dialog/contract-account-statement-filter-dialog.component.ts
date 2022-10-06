import { Component, OnInit } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { ModalDialogParams } from '@nativescript/angular';

import { BaseFormHelper } from '../../../_helpers/base_helpers/base-form.helper';

import { AccountStatementFilterFormSource } from '../../../_models/form/account-statement-filter-form.model';
import { AccountStatementFilter } from '../../../_models/filters/account-statement-filter.model';

import { AccountStatementTypeEnum } from '../../../_enum/account-statement-type.enum';
import { PaymentStatusEnum } from '../../../_enum/payment-status.enum';
import { SortOrderEnum } from '../../../_enum/sort-order.enum';

import { AccountStatementTypeValueProvider } from '../../../_helpers/value_providers/account-statement-type-value-provider.helper';
import { SortOrderValueProvider } from '../../../_helpers/value_providers/sort-order-value-provider.helper';
import { PaymentStatusValueProvider } from '../../../_helpers/value_providers/payment-status-value-provider.helper';

@Component({
  selector: 'ns-contract-account-statement-filter-dialog',
  templateUrl: './contract-account-statement-filter-dialog.component.html',
  styleUrls: ['./contract-account-statement-filter-dialog.component.scss'],
})
export class ContractAccountStatementFilterDialogComponent
  extends BaseFormHelper<AccountStatementFilterFormSource>
  implements OnInit
{
  private _accountStatementFilterData: AccountStatementFilter;
  private _accountStatementFilterFormSource: AccountStatementFilterFormSource;
  private _accountStatementFilterFormSourceOriginal: AccountStatementFilterFormSource;

  private _sortOrderValueProvider: SortOrderValueProvider;
  private _paymentStatusValueProvider: PaymentStatusValueProvider;
  private _accountStatementTypeValueProvider: AccountStatementTypeValueProvider;

  constructor(
    private params: ModalDialogParams,
    private translateService: TranslateService
  ) {
    super();
    this._accountStatementFilterData = <AccountStatementFilter>(
      params.context.accountStatementFilter
    );
    this._accountStatementFilterFormSource =
      new AccountStatementFilterFormSource(
        this._accountStatementFilterData.fromDate,
        this._accountStatementFilterData.toDate,
        this._accountStatementFilterData.paymentStatus,
        this._accountStatementFilterData.accountStatementType,
        this._accountStatementFilterData.sortOrder
      );

    this._accountStatementFilterFormSourceOriginal =
      this._accountStatementFilterFormSource.clone();
  }

  ngOnInit() {
    this._sortOrderValueProvider = new SortOrderValueProvider(
      this.translateService
    );

    this._paymentStatusValueProvider = new PaymentStatusValueProvider(
      this.translateService
    );

    this._accountStatementTypeValueProvider =
      new AccountStatementTypeValueProvider(this.translateService, false, true);
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
    this._accountStatementFilterData.fromDate =
      this._accountStatementFilterFormSource.fromDate;
    this._accountStatementFilterData.toDate =
      this._accountStatementFilterFormSource.toDate;
    this._accountStatementFilterData.paymentStatus =
      this._accountStatementFilterFormSource.paymentStatus;
    this._accountStatementFilterData.accountStatementType =
      this._accountStatementFilterFormSource.accountStatementType;
    this._accountStatementFilterData.sortOrder =
      this._accountStatementFilterFormSource.sortOrder;
  }

  resetFilterValues() {
    this._accountStatementFilterFormSource = this.reloadFormSource(
      this._accountStatementFilterFormSource,
      <AccountStatementFilter>{
        fromDate: null,
        toDate: null,
        paymentStatus: PaymentStatusEnum.All,
        accountStatementType: AccountStatementTypeEnum.All,
        sortOrder: SortOrderEnum.Descending,
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

  get accountStatementTypeOptions(): Array<{
    key: AccountStatementTypeEnum;
    label: string;
  }> {
    return this._accountStatementTypeValueProvider.accountStatementTypeOptions;
  }

  get paymentStatusOptions(): Array<{ key: PaymentStatusEnum; label: string }> {
    return this._paymentStatusValueProvider.paymentStatusOptions;
  }
}
