import { Component, OnInit } from '@angular/core';

import { ModalDialogParams } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { ArchivedContractStatusEnum } from '@src/app/_enum/archived-contract-options.enum';
import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';

import { BaseFormHelper } from '@src/app/_helpers/base_helpers/base-form.helper';
import { SortOrderValueProvider } from '@src/app/_helpers/value_providers/sort-order-value-provider.helper';
import { ArchivedContractStatusValueProvider } from '@src/app/_helpers/value_providers/archived-contract-status-value-provider.helper';

import { ContractFilter } from '@src/app/_models/filters/contract-filter.model';
import { ArchivedContractFilterFormSource } from '@src/app/_models/form/archived-contract-filter-form.model';

@Component({
  selector: 'app-contract-archived-filter-dialog',
  templateUrl: './contract-archived-filter-dialog.component.html',
  styleUrls: ['./contract-archived-filter-dialog.component.scss'],
})
export class ContractArchivedFilterDialogComponent
  extends BaseFormHelper<ArchivedContractFilterFormSource>
  implements OnInit
{
  private _contractFilterData: ContractFilter;
  private _sortOrderValueProvider: SortOrderValueProvider;

  private _archivedContractStatusValueProvider: ArchivedContractStatusValueProvider;

  private _contractFilterFormSource: ArchivedContractFilterFormSource;
  private _contractFilterFormSourceOriginal: ArchivedContractFilterFormSource;

  constructor(
    private params: ModalDialogParams,
    private translateService: TranslateService
  ) {
    super();
    this._contractFilterData = <ContractFilter>params.context.contractFilter;
    this._contractFilterFormSource = new ArchivedContractFilterFormSource(
      this._contractFilterData.archivedContractStatus,
      this._contractFilterData.codeSortOrder
    );
  }

  ngOnInit() {
    this._archivedContractStatusValueProvider =
      new ArchivedContractStatusValueProvider(this.translateService);

    this._sortOrderValueProvider = new SortOrderValueProvider(
      this.translateService
    );
  }

  initFilterOptions(contractFilter: ContractFilter) {
    if (contractFilter.archivedContractStatus) {
      this._contractFilterFormSource.archivedContractStatus = Number(
        contractFilter.archivedContractStatus
      );
    }

    if (contractFilter.codeSortOrder) {
      this._contractFilterFormSource.codeSortOrder =
        contractFilter.codeSortOrder;
    }
  }

  onConfirm() {
    const filterHasChanged = !this._contractFilterFormSource.isSame(
      this._contractFilterFormSourceOriginal
    );
    if (filterHasChanged) {
      this.setFilterValues();
    }
    this.params.closeCallback({
      filterHasChanged: filterHasChanged,
      filter: this._contractFilterData,
    });
  }

  onReset() {
    this.resetFilterValues();
  }

  setFilterValues() {
    this._contractFilterData.archivedContractStatus =
      this._contractFilterFormSource.archivedContractStatus;
    this._contractFilterData.codeSortOrder =
      this._contractFilterFormSource.codeSortOrder;
  }

  resetFilterValues() {
    this._contractFilterFormSource = this.reloadFormSource(
      this._contractFilterFormSource,
      <ArchivedContractFilterFormSource>{
        archivedContractStatus: ArchivedContractStatusEnum.All,
        codeSortOrder: SortOrderEnum.Descending,
      }
    );
  }

  closeDialog() {
    this.params.closeCallback();
  }

  get contractFilterFormSource() {
    return this._contractFilterFormSource;
  }

  get sortOrderOptions(): Array<{ key: SortOrderEnum; label: string }> {
    return this._sortOrderValueProvider.sortOrderOptions;
  }

  get statusOptions(): Array<{
    key: ArchivedContractStatusEnum;
    label: string;
  }> {
    return this._archivedContractStatusValueProvider.statusOptions;
  }
}
