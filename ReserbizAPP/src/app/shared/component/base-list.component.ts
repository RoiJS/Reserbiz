import { Location } from '@angular/common';
import { Component, OnInit, ViewChild, OnDestroy, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { RouterExtensions } from 'nativescript-angular/router';

import { View, isAndroid } from 'tns-core-modules/ui/page/page';
import { RadListViewComponent } from 'nativescript-ui-listview/angular/listview-directives';
import {
  ListViewEventData,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';
import { ObservableArray } from 'tns-core-modules/data/observable-array/observable-array';

import { Subscription } from 'rxjs';
import { take, finalize } from 'rxjs/operators';

import { IEntity } from '@src/app/_interfaces/ientity.interface';
import { IBaseDialogTexts } from '@src/app/_interfaces/ibase-dialog-texts.interface';
import { IBaseService } from '@src/app/_interfaces/ibase-service.interface';
import { IEntityFilter } from '@src/app/_interfaces/ientity-filter.interface';

import { DialogService } from '@src/app/_services/dialog.service';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { ExtendedNavigationExtras } from 'nativescript-angular/router/router-extensions';
import { ActivatedRoute } from '@angular/router';

@Component({
  template: ``,
})
export class BaseListComponent<TEntity extends IEntity>
  implements OnInit, OnDestroy {
  @ViewChild('appListView', { static: false })
  appListView: RadListViewComponent;

  protected _listItems: ObservableArray<TEntity>;

  protected _currentItem: TEntity;

  protected _loadListFlagSub: Subscription;

  protected _multipleSelectionActive = false;

  protected _isBusy = false;

  protected _isCurrentItemActive = false;

  protected _isCurrentItemDeletable = true;

  protected _navigateBackSub: any;

  protected _isNotNavigateToOtherPage = true;

  protected _itemListHasLoaded = false;

  protected _deleteMultipleItemsDialogTexts: IBaseDialogTexts = null;

  protected _deleteItemDialogTexts: IBaseDialogTexts = null;

  protected _activateItemDialogTexts: IBaseDialogTexts = null;

  protected _deactivateItemDialogTexts: IBaseDialogTexts = null;

  protected activatedRoute: ActivatedRoute;

  protected entityService: IBaseService<TEntity>;

  constructor(
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected router: RouterExtensions,
    protected translateService: TranslateService
  ) {}

  ngOnInit() {
    this._navigateBackSub = this.location.subscribe(() => {
      this._isNotNavigateToOtherPage = true;
      this._multipleSelectionActive = false;
    });
  }

  ngOnDestroy() {
    if (this._navigateBackSub) {
      this._navigateBackSub.unsubscribe();
    }

    if (this._loadListFlagSub) {
      this._loadListFlagSub.unsubscribe();
    }
  }

  setEntityService(entityService: IBaseService<TEntity>) {
    this.entityService = entityService;
  }

  getEntities(
    entityFilter: IEntityFilter = null,
    onGetListFinished?: () => void
  ) {
    this._isBusy = true;
    setTimeout(() => {
      this.entityService
        .getEntities(entityFilter)
        .pipe(
          take(1),
          finalize(() => {
            this.ngZone.run(() => {
              this._isBusy = false;
            });
          })
        )
        .subscribe((entities: TEntity[]) => {
          this._listItems = new ObservableArray<TEntity>(entities);

          this._multipleSelectionActive = false;

          this._isNotNavigateToOtherPage = true;

          this._itemListHasLoaded = true;

          if (onGetListFinished) {
            onGetListFinished();
          }
        });
    }, 500);
  }

  selectItem(currentIndex: number, url: string) {
    // Stores the selected item information
    const selectedItem = <TEntity>this._listItems.getItem(currentIndex);

    // If multiple item selection is active,
    // do not allow navigation to details page.
    if (!this._multipleSelectionActive) {
      // Set this variable to false which indicates that the user is
      // about to navigate to other page.
      this._isNotNavigateToOtherPage = false;

      // Performs navigation to other page.
      // Setting delay is necessary to be able to make
      // the swipe options on the component template to prevent showing
      // when navigating to other page.
      this.navigateToOtherPage(url.replace(':id', selectedItem.id.toString()));
    } else {
      // This will prevent the item from being selected
      if (!selectedItem.isDeletable) {
        this.appListView.listView.deselectItemAt(currentIndex);
        return;
      }

      selectedItem.isSelected = true;
    }
  }

  navigateToOtherPage(url: string) {
    setTimeout(() => {
      const routeConfig: ExtendedNavigationExtras = {
        transition: {
          name: 'slideLeft',
        },
      };

      if (this.activatedRoute) {
        routeConfig.relativeTo = this.activatedRoute;
      }

      this.router.navigate([url], routeConfig);
    }, 100);
  }

  deselectedItem(args: ListViewEventData) {
    // Set isSelected to false for the selected tenant
    const selectedItem = <TEntity>this._listItems.getItem(args.index);
    selectedItem.isSelected = false;
  }

  activateDeactivateMultipleSelection() {
    if (this._listItems.length === 0) {
      return;
    }

    // Activate and deactivate _multipleSelectionActive
    this._multipleSelectionActive = !this._multipleSelectionActive;

    // if _multipleSelectionActive is set to false,
    // We need to make sure that no tenants from the list
    // are selected.
    if (!this._multipleSelectionActive) {
      this._listItems.map((item) => {
        item.isSelected = false;
      });

      // Deselect all items via Radlistview
      this.appListView.listView.deselectAll();
    }
  }

  /**
   * @description Delete multiple selected items from the list.
   */
  deleteSelectedItems() {
    const me = this;
    const selectedItems = this.appListView.listView.getSelectedItems();
    if (selectedItems.length > 0) {
      this.dialogService
        .confirm(
          this._deleteMultipleItemsDialogTexts.title,
          this._deleteMultipleItemsDialogTexts.confirmMessage
        )
        .then((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this.entityService
              .deleteMultipleItems(selectedItems)
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this._deleteMultipleItemsDialogTexts.title,
                    this._deleteMultipleItemsDialogTexts.successMessage
                  );

                  me._listItems = new ObservableArray<TEntity>([]);
                  me.getEntities();
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this._deleteMultipleItemsDialogTexts.title,
                    this._deleteMultipleItemsDialogTexts.errorMessage
                  );
                }
              );
          }
        });
    }
  }

  /**
   * @description Delete single selected item
   */
  deleteSelectedItem(event: any) {
    const selectedItemIndex = (<any>this.appListView.listView).getIndexOf(
      this._currentItem
    );

    this.dialogService
      .confirm(
        this._deleteItemDialogTexts.title,
        this._deleteItemDialogTexts.confirmMessage
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.entityService
            .deleteItem(this._currentItem.id)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this._deleteItemDialogTexts.title,
                  this._deleteItemDialogTexts.successMessage,
                  () => {
                    (<any>this._listItems).splice(selectedItemIndex, 1);
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this._deleteItemDialogTexts.title,
                  this._deleteItemDialogTexts.errorMessage
                );
              }
            );
        }
      });
  }

  /**
   * @description Activates selected tenant
   */
  activateSelectedItem() {
    this.dialogService
      .confirm(
        this._activateItemDialogTexts.title,
        this._activateItemDialogTexts.confirmMessage
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.entityService
            .setEntityStatus(this._currentItem.id, true)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this._activateItemDialogTexts.title,
                  this._activateItemDialogTexts.successMessage
                );

                this.appListView.listView.notifySwipeToExecuteFinished();
                this._listItems = new ObservableArray<TEntity>([]);
                this.getEntities();
              },
              (error: Error) => {
                this.dialogService.alert(
                  this._activateItemDialogTexts.title,
                  this._activateItemDialogTexts.errorMessage
                );
              }
            );
        }
      });
  }

  /**
   * @description Deactivates selected item
   */
  deactivateSelectedItem() {
    this.dialogService
      .confirm(
        this._deactivateItemDialogTexts.title,
        this._deactivateItemDialogTexts.confirmMessage
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.entityService
            .setEntityStatus(this._currentItem.id, false)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this._deactivateItemDialogTexts.title,
                  this._deactivateItemDialogTexts.successMessage
                );

                this.appListView.listView.notifySwipeToExecuteFinished();
                this._listItems = new ObservableArray<TEntity>([]);
                this.getEntities();
              },
              (error: Error) => {
                this.dialogService.alert(
                  this._deactivateItemDialogTexts.title,
                  this._deactivateItemDialogTexts.errorMessage
                );
              }
            );
        }
      });
  }

  /**
   * @description RadListView.itemSwipeProgressStarted event handler. Configures swipe item threshold.
   * @param args ListViewEventData object
   */
  onSwipeCellStarted(args: ListViewEventData) {
    this._currentItem = (<SwipeActionsEventData>args).mainView
      .bindingContext as TEntity;
    this._isCurrentItemActive = this._currentItem.isActive;
    this._isCurrentItemDeletable = this._currentItem.isDeletable;
    const swipeLimits = args.data.swipeLimits;
    const swipeView = args['object'];
    const itemSwipeActions = swipeView.getViewById<View>('itemSwipeActions');
    swipeLimits.right = itemSwipeActions.getMeasuredWidth();
    swipeLimits.threshold = itemSwipeActions.getMeasuredWidth();
  }

  /**
   * @description RadListView.pullToRefreshInitiated event handler. Triggers pull to refresh action.
   * @param args ListViewEventData
   */
  onPullToRefreshInitiated(args: ListViewEventData) {
    this.getEntities(null, () => {
      args.object.notifyPullToRefreshFinished();
    });
  }

  searchBarLoaded(args: any) {
    const searchbar = args.object;
    if (isAndroid) {
      searchbar.android.clearFocus();
    }
  }

  onClearSearchText(args: any) {
    if (this._itemListHasLoaded) {
      args.object.text = '';
      this.getEntities({ searchKeyword: args.object.text });
    }
  }

  onSubmitSearchText(args: any) {
    this.getEntities({ searchKeyword: args.object.text });
  }

  get items(): ObservableArray<TEntity> {
    return this._listItems;
  }

  get multipleSelectionActive(): boolean {
    return this._multipleSelectionActive;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }

  get selectedCount(): string {
    return `${
      this.appListView.listView.getSelectedItems().length
    } ${this.translateService.instant('GENERAL_TEXTS.SELECTED_ITEMS')}`;
  }

  get isCurrentItemActive(): boolean {
    return this._isCurrentItemActive;
  }

  get isCurrentItemDeletable(): boolean {
    return this._isCurrentItemDeletable;
  }

  get isNotNavigateToOtherPage(): boolean {
    return this._isNotNavigateToOtherPage;
  }
}
