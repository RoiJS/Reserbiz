import { Location } from '@angular/common';

import {
  Component,
  NgZone,
  OnDestroy,
  OnInit,
  ViewContainerRef,
} from '@angular/core';
import {
  ModalDialogOptions,
  ModalDialogService,
  RouterExtensions,
} from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';

import { IBaseListComponent } from '@src/app/_interfaces/components/ibase-list-component.interface';

import { UserNotificationFilter } from '@src/app/_models/filters/user-notification-filter.model';
import { UserNotification } from '@src/app/_models/user-notification.model';

import { DialogService } from '@src/app/_services/dialog.service';
import { StorageService } from '@src/app/_services/storage.service';
import { UserNotificationService } from '@src/app/_services/user-notification.service';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';
import { NotificationFilterDialogComponent } from '../notification-filter-dialog/notification-filter-dialog.component';

@Component({
  selector: 'app-notifications',
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.scss'],
})
export class NotificationListComponent
  extends BaseListComponent<UserNotification>
  implements OnInit, OnDestroy
{
  private _templateSelector: (
    item: UserNotification,
    index: number,
    items: any
  ) => string;

  private NOTIFICATION_FILTER_FROM = 'notificationFilter_from';
  private NOTIFICATION_FILTER_TO = 'notificationFilter_to';
  private NOTIFICATION_FILTER_SORT_ORDER = 'notificationFilter_sortOrder';

  constructor(
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected router: RouterExtensions,
    protected translateService: TranslateService,
    private userNotificationService: UserNotificationService,
    private storageService: StorageService,
    private modalDialogService: ModalDialogService,
    private vcRef: ViewContainerRef
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = userNotificationService;

    this._entityFilter = new UserNotificationFilter();
    this._entityFilter.page = 1;
  }

  ngOnInit() {
    this._loadListFlagSub =
      this.userNotificationService.loadUserNotificationListFlag.subscribe(
        () => {
          this.initFilterOptions();
          this._templateSelector = this.templateSelectorFunction;
          this.getPaginatedEntities();
        }
      );

    super.ngOnInit();
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  selectItem(currentIndex: number) {
    const selectedItem = <UserNotification>(
      this._listItems.getItem(currentIndex)
    );

    if (selectedItem.id > 0) {
      // Check if current selected item is unread
      // then mark it as read.
      if (!selectedItem.readStatus) {
        selectedItem.readStatus = true;
      }

      super.selectItem(currentIndex, selectedItem.notificationUrl);
    }
  }

  initFilterOptions() {
    const fromDate = this.storageService.getString(
      this.NOTIFICATION_FILTER_FROM
    );
    const toDate = this.storageService.getString(this.NOTIFICATION_FILTER_TO);
    const sortOrder = this.storageService.getString(
      this.NOTIFICATION_FILTER_SORT_ORDER
    );

    if (fromDate) {
      (<UserNotificationFilter>this._entityFilter).fromDate = new Date(
        fromDate
      );
    }

    if (toDate) {
      (<UserNotificationFilter>this._entityFilter).toDate = new Date(toDate);
    }

    if (sortOrder) {
      (<UserNotificationFilter>this._entityFilter).sortOrder = <SortOrderEnum>(
        parseInt(sortOrder)
      );
    }
  }

  storeFilterOptions(contractFilter: UserNotificationFilter) {
    const fromDate = contractFilter.fromDate;
    const toDate = contractFilter.toDate;
    const sortOrder = contractFilter.sortOrder;

    if (fromDate) {
      this.storageService.storeString(
        this.NOTIFICATION_FILTER_FROM,
        fromDate.toString()
      );
    }

    if (toDate) {
      this.storageService.storeString(
        this.NOTIFICATION_FILTER_TO,
        toDate.toString()
      );
    }

    if (sortOrder) {
      this.storageService.storeString(
        this.NOTIFICATION_FILTER_SORT_ORDER,
        sortOrder.toString()
      );
    }
  }

  resetFilterOptions() {
    this.storageService.remove(this.NOTIFICATION_FILTER_FROM);
    this.storageService.remove(this.NOTIFICATION_FILTER_TO);
    this.storageService.remove(this.NOTIFICATION_FILTER_SORT_ORDER);
  }

  initFilterDialog(): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        notificationFilter: <UserNotificationFilter>this._entityFilter,
      },
      fullscreen: false,
      animated: true,
    };

    return this.modalDialogService.showModal(
      NotificationFilterDialogComponent,
      dialogOptions
    );
  }

  openFilterDialog() {
    this.initFilterDialog().then(
      (data: { filterHasChanged: boolean; filter: UserNotificationFilter }) => {
        if (!data) {
          return;
        }

        if (!data.filterHasChanged) {
          return;
        }

        if (data.filter.isFilterActive()) {
          this.storeFilterOptions(data.filter);
        } else {
          (<UserNotificationFilter>this._entityFilter).reset();
          this.resetFilterOptions();
        }

        // Needs to reset the page number to get
        // the correct data
        this._entityFilter.page = 1;
        this.entityService.reloadListFlag();
      }
    );
  }

  get templateSelector(): (
    item: UserNotification,
    index: number,
    items: any
  ) => string {
    return this._templateSelector;
  }

  set templateSelector(
    value: (item: UserNotification, index: number, items: any) => string
  ) {
    this._templateSelector = value;
  }

  public templateSelectorFunction = (
    item: UserNotification,
    index: number,
    items: any
  ) => {
    return item.notificationItemTypeIdentifier;
  };
}
