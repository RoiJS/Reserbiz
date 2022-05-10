import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDialogParams } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';

import { BaseFormHelper } from '../../_helpers/base_helpers/base-form.helper';
import { SortOrderValueProvider } from '../../_helpers/value_providers/sort-order-value-provider.helper';

import { NotificationFilterFormSource } from '../../_models/form/notification-filter-form.model';
import { UserNotificationFilter } from '../../_models/filters/user-notification-filter.model';

import { SortOrderEnum } from '../../_enum/sort-order.enum';

@Component({
  selector: 'app-notification-filter-dialog',
  templateUrl: './notification-filter-dialog.component.html',
  styleUrls: ['./notification-filter-dialog.component.scss'],
})
export class NotificationFilterDialogComponent
  extends BaseFormHelper<NotificationFilterFormSource>
  implements OnInit
{
  @ViewChild(RadDataFormComponent, { static: false })
  notificationFilterForm: RadDataFormComponent;

  private _notificationFilterData: UserNotificationFilter;
  private _notificationFilterFormSource: NotificationFilterFormSource;
  private _notificationFilterFormSourceOriginal: NotificationFilterFormSource;

  private _sortOrderValueProvider: SortOrderValueProvider;

  constructor(
    private params: ModalDialogParams,
    private translateService: TranslateService
  ) {
    super();
    this._notificationFilterData = <UserNotificationFilter>(
      params.context.notificationFilter
    );
    this._notificationFilterFormSource = new NotificationFilterFormSource(
      this._notificationFilterData.fromDate,
      this._notificationFilterData.toDate,
      this._notificationFilterData.sortOrder
    );

    this._notificationFilterFormSourceOriginal =
      this._notificationFilterFormSource.clone();
    this.initFilterOptions(this._notificationFilterData);
  }

  ngOnInit() {
    this._sortOrderValueProvider = new SortOrderValueProvider(
      this.translateService
    );
  }

  initFilterOptions(notificationFilter: UserNotificationFilter) {
    if (notificationFilter.fromDate) {
      this._notificationFilterFormSource.fromDate = new Date(
        notificationFilter.fromDate
      );
    }

    if (notificationFilter.toDate) {
      this._notificationFilterFormSource.toDate = new Date(
        notificationFilter.toDate
      );
    }

    if (notificationFilter.sortOrder) {
      this._notificationFilterFormSource.sortOrder =
        notificationFilter.sortOrder;
    }
  }

  onConfirm() {
    const filterHasChanged = !this._notificationFilterFormSource.isSame(
      this._notificationFilterFormSourceOriginal
    );
    if (filterHasChanged) {
      this.setFilterValues();
    }
    this.params.closeCallback({
      filterHasChanged: filterHasChanged,
      filter: this._notificationFilterData,
    });
  }

  onReset() {
    this.resetFilterValues();
  }

  setFilterValues() {
    this._notificationFilterData.fromDate =
      this._notificationFilterFormSource.fromDate;
    this._notificationFilterData.toDate =
      this._notificationFilterFormSource.toDate;
    this._notificationFilterData.sortOrder =
      this._notificationFilterFormSource.sortOrder;
  }

  resetFilterValues() {
    this._notificationFilterFormSource = this.reloadFormSource(
      this._notificationFilterFormSource,
      <NotificationFilterFormSource>{
        fromDate: null,
        toDate: null,
        sortOrder: SortOrderEnum.Descending,
      }
    );
  }

  closeDialog() {
    this.params.closeCallback();
  }

  get notificationFilterFormSource() {
    return this._notificationFilterFormSource;
  }

  get sortOrderOptions(): Array<{ key: SortOrderEnum; label: string }> {
    return this._sortOrderValueProvider.sortOrderOptions;
  }
}
