import { Component, OnInit } from '@angular/core';
import { ModalDialogParams } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { PaymentForTypeEnum } from '@src/app/_enum/payment-type.enum';
import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';

import { BaseFormHelper } from '@src/app/_helpers/base_helpers/base-form.helper';

import { PaymentTypeValueProvider } from '@src/app/_helpers/value_providers/payment-type-value-provider.helper';
import { SortOrderValueProvider } from '@src/app/_helpers/value_providers/sort-order-value-provider.helper';

import { PaymentFilter } from '@src/app/_models/filters/payment-filter.model';
import { PaymentFilterFormSource } from '@src/app/_models/form/payment-filter-form.model';

@Component({
  selector: 'app-payment-filter-dialog',
  templateUrl: './payment-filter-dialog.component.html',
  styleUrls: ['./payment-filter-dialog.component.scss'],
})
export class PaymentFilterDialogComponent
  extends BaseFormHelper<PaymentFilterFormSource>
  implements OnInit
{
  private _paymentFilterData: PaymentFilter;
  private _paymentFilterFormSource: PaymentFilterFormSource;
  private _paymentFilterFormSourceOriginal: PaymentFilterFormSource;

  private _sortOrderValueProvider: SortOrderValueProvider;
  private _paymentForTypeValueProvider: PaymentTypeValueProvider;

  constructor(
    private params: ModalDialogParams,
    private translateService: TranslateService
  ) {
    super();
    this._paymentFilterData = <PaymentFilter>(
      params.context.paymentFilter
    );
    this._paymentFilterFormSource = new PaymentFilterFormSource(
      this._paymentFilterData.paymentForType,
      this._paymentFilterData.sortOrder
    );

    this._paymentFilterFormSourceOriginal =
      this._paymentFilterFormSource.clone();
  }

  ngOnInit() {
    this._sortOrderValueProvider = new SortOrderValueProvider(
      this.translateService
    );

    this._paymentForTypeValueProvider = new PaymentTypeValueProvider(
      this.translateService,
      true
    );
  }

  onConfirm() {
    const filterHasChanged = !this._paymentFilterFormSource.isSame(
      this._paymentFilterFormSourceOriginal
    );
    if (filterHasChanged) {
      this.setFilterValues();
    }
    this.params.closeCallback({
      filterHasChanged: filterHasChanged,
      filter: this._paymentFilterData,
    });
  }

  onReset() {
    this.resetFilterValues();
  }

  setFilterValues() {
    this._paymentFilterData.paymentForType =
      this._paymentFilterFormSource.paymentForType;
    this._paymentFilterData.sortOrder = this._paymentFilterFormSource.sortOrder;
  }

  resetFilterValues() {
    this._paymentFilterFormSource = this.reloadFormSource(
      this._paymentFilterFormSource,
      <PaymentFilter>{
        paymentForType: PaymentForTypeEnum.All,
        sortOrder: SortOrderEnum.Descending,
      }
    );
  }

  closeDialog() {
    this.params.closeCallback();
  }

  get paymentFilterFormSource() {
    return this._paymentFilterFormSource;
  }

  get sortOrderOptions(): Array<{ key: SortOrderEnum; label: string }> {
    return this._sortOrderValueProvider.sortOrderOptions;
  }

  get paymentForTypeOptions(): Array<{
    key: PaymentForTypeEnum;
    label: string;
  }> {
    return this._paymentForTypeValueProvider.paymentTypeOptions;
  }
}
