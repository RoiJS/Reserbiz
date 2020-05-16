import { Location } from '@angular/common';
import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import {
  ListViewEventData,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';
import { RadListViewComponent } from 'nativescript-ui-listview/angular/listview-directives';
import { RouterExtensions } from 'nativescript-angular/router';

import { ObservableArray } from 'tns-core-modules/data/observable-array';
import { View, isAndroid } from 'tns-core-modules/ui/page/page';

import { finalize, take } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { TenantService } from '@src/app/_services/tenant.service';
import { Tenant } from '@src/app/_models/tenant.model';
import { DialogService } from '@src/app/_services/dialog.service';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-tenant-list',
  templateUrl: './tenant-list.component.html',
  styleUrls: ['./tenant-list.component.scss'],
})
export class TenantListComponent implements OnInit, OnDestroy {
  // Reference to RadListView on the template
  @ViewChild('tenantListView', { static: false })
  tenantListView: RadListViewComponent;

  // Holds list of tenants
  private _tenants: ObservableArray<Tenant>;

  // flag indicates whether multiple item selection is active or not. False as default
  private _multipleSelectionActive = false;

  // flag indicates server related request action is still in progress or not. False as default.
  private _isBusy = false;

  // Holds the current selected tenant information
  private _currentTenant: Tenant;

  // Flag indicates whether the current selected tenant is active or not.
  private _isCurrentTenantActive = false;

  private _isCurrentTenantDeletable = true;

  // This will store the location service subscription.
  private _loadTenantListFlagSub: Subscription;

  private _navigateBackSub: any;

  // Flag inidicates whether the current page is navigating to other page.
  private _isNotNavigateToOtherPage = true;

  private _tenantListHasLoaded = false;

  constructor(
    private dialogService: DialogService,
    private location: Location,
    private tenantService: TenantService,
    private translateService: TranslateService,
    private router: RouterExtensions
  ) {}

  ngOnInit() {
    this._loadTenantListFlagSub = this.tenantService.loadTenantListFlag.subscribe(
      () => {
        this.getTenantList();
      }
    );

    this._navigateBackSub = this.location.subscribe(() => {
      this._isNotNavigateToOtherPage = true;
    });
  }

  ngOnDestroy() {
    this._navigateBackSub.unsubscribe();
    this._loadTenantListFlagSub.unsubscribe();
  }

  /**
   * @description Retrieve list of tenants from server
   * @param onGetListFinished Optional parameter function trigger after retrieving list of tenants
   */
  getTenantList(tenantName: string = '', onGetListFinished?: () => void) {
    this._isBusy = true;
    setTimeout(() => {
      this.tenantService
        .getTenants(tenantName)
        .pipe(
          take(1),
          finalize(() => (this._isBusy = false))
        )
        .subscribe((tenants) => {
          // Store list of tenants from server
          this._tenants = new ObservableArray<Tenant>(tenants);

          // set flag indicating multiple tenant selection is not active
          this._multipleSelectionActive = false;

          this._isNotNavigateToOtherPage = true;

          this._tenantListHasLoaded = true;

          if (onGetListFinished) {
            onGetListFinished();
          }
        });
    }, 500);
  }

  /**
   * @description Navigates user to tenant details page
   * @param args ListViewEventData parameter
   */
  goToTenantDetails(args: ListViewEventData) {
    // Stores the selected tenant information
    const selectedTenant = this._tenants.getItem(args.index);

    // If multiple item selection is active,
    // do not allow navigation to details page.
    if (!this._multipleSelectionActive) {
      // Set this variable to false which indicates that the user is
      // about to navigate to other page.
      this._isNotNavigateToOtherPage = false;

      // Performs navigation to other page.
      // Settig delay is necessary to be able to make
      // the swipe options on the component template to prevent showing
      // when navigating to other page.
      setTimeout(() => {
        this.router.navigate([`/tenants/${selectedTenant.id}`], {
          transition: {
            name: 'slideLeft',
          },
        });
      }, 100);
    } else {
      // This will prevent the item from being selected
      if (!selectedTenant.isDeletable) {
        this.tenantListView.listView.deselectItemAt(args.index);
        return;
      }

      selectedTenant.isSelected = true;
    }
  }

  /**
   * @description Navigates to add tenant details page
   */
  goToAddTenantsPage() {
    this.router.navigate(['/tenants/add-tenant'], {
      transition: {
        name: 'slideLeft',
      },
    });
  }

  /**
   * @description RadListView.itemDeselected event handler. Performs deselection of current tenant item.
   * @param args ListViewEventData object
   */
  deselectedItem(args: ListViewEventData) {
    // Set isSelected to false for the selected tenant
    const selectedTenant = this._tenants.getItem(args.index);
    selectedTenant.isSelected = false;
  }

  /**
   * @description This will either activates or deactivates flag indicating multiple selection item is active or not.
   */
  activateDeactivateMultipleSelection() {
    if (this._tenants.length === 0) {
      return;
    }

    // Activate and deactivate _multipleSelectionActive
    this._multipleSelectionActive = !this._multipleSelectionActive;

    // if _multipleSelectionActive is set to false,
    // We need to make sure that no tenants from the list
    // are selected.
    if (!this._multipleSelectionActive) {
      this._tenants.map((tenant) => {
        tenant.isSelected = false;
      });

      // Deselect all items via Radlistview
      this.tenantListView.listView.deselectAll();
    }
  }

  /**
   * @description Delete multiple selected tenants from the list.
   */
  deleteSelectedTenants() {
    const me = this;
    const selectedTenants = this.tenantListView.listView.getSelectedItems();
    if (selectedTenants.length > 0) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this.tenantService
              .deleteMultipleTenants(selectedTenants)
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.SUCCESS_MESSAGE'
                    )
                  );

                  me._tenants = new ObservableArray<Tenant>([]);
                  me.getTenantList();
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.ERROR_MESSAGE'
                    )
                  );
                }
              );
          }
        });
    }
  }

  /**
   * @description Delete single selected tenant
   */
  deleteSelectedTenant(event: any) {
    const selectedTenantIndex = (<any>this.tenantListView.listView).getIndexOf(
      this._currentTenant
    );

    this.dialogService
      .confirm(
        this.translateService.instant(
          'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.tenantService
            .deleteTenant(this._currentTenant.id)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    (<any>this._tenants).splice(selectedTenantIndex, 1);
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  /**
   * @description Activates selected tenant
   */
  activateSelectedTenant() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.tenantService
            .setTenantStatus(this._currentTenant.id, true)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
                  )
                );

                this.tenantListView.listView.notifySwipeToExecuteFinished();
                this._tenants = new ObservableArray<Tenant>([]);
                this.getTenantList();
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  /**
   * @description Deactivates selected tenant
   */
  deactivateSelectedTenant() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.tenantService
            .setTenantStatus(this._currentTenant.id, false)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
                  )
                );

                this.tenantListView.listView.notifySwipeToExecuteFinished();
                this._tenants = new ObservableArray<Tenant>([]);
                this.getTenantList();
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
                  )
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
    this._currentTenant = (<SwipeActionsEventData>args).mainView
      .bindingContext as Tenant;
    this._isCurrentTenantActive = this._currentTenant.isActive;
    this._isCurrentTenantDeletable = this._currentTenant.isDeletable;
    const swipeLimits = args.data.swipeLimits;
    const swipeView = args['object'];
    const tenantSwipeActions = swipeView.getViewById<View>(
      'tenantSwipeActions'
    );
    swipeLimits.right = tenantSwipeActions.getMeasuredWidth();
    swipeLimits.threshold = tenantSwipeActions.getMeasuredWidth();
  }

  /**
   * @description RadListView.pullToRefreshInitiated event handler. Triggers pull to refresh action.
   * @param args ListViewEventData
   */
  onPullToRefreshInitiated(args: ListViewEventData) {
    this.getTenantList('', () => {
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
    if (this._tenantListHasLoaded) {
      args.object.text = '';
      this.getTenantList(args.object.text);
    }
  }

  onSubmitSearchText(args: any) {
    this.getTenantList(args.object.text);
  }

  get tenants(): ObservableArray<Tenant> {
    return this._tenants;
  }

  get multipleSelectionActive(): boolean {
    return this._multipleSelectionActive;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }

  get selectedCount(): string {
    return `${
      this.tenantListView.listView.getSelectedItems().length
    } ${this.translateService.instant('TENANTS_LIST_PAGE.SELECTED_TENANTS')}`;
  }

  get isCurrentTenantActive(): boolean {
    return this._isCurrentTenantActive;
  }

  get isCurrentTenantDeletable(): boolean {
    return this._isCurrentTenantDeletable;
  }

  get isNotNavigateToOtherPage(): boolean {
    return this._isNotNavigateToOtherPage;
  }
}
