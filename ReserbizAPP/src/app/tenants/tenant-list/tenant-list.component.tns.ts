import { Location } from '@angular/common';
import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import {
  ListViewEventData,
  RadListView,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';
import { RadListViewComponent } from 'nativescript-ui-listview/angular/listview-directives';
import { RouterExtensions } from 'nativescript-angular/router';

import { ObservableArray } from 'tns-core-modules/data/observable-array';
import { View, layout } from 'tns-core-modules/ui/page/page';

import { finalize, take } from 'rxjs/operators';

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

  // This will store the location service subscription.
  private _locationSub: any;

  // Flag inidicates whether the current page is navigating to other page.
  private _isNotNavigateToOtherPage = true;

  constructor(
    private dialogService: DialogService,
    private tenantService: TenantService,
    private translateService: TranslateService,
    private router: RouterExtensions,
    private location: Location
  ) {}

  ngOnInit() {
    // Initial call when first visit the page
    this.getTenantList();

    // Event listener when navigating back from previous page.
    this._locationSub = this.location.subscribe(() => {
      this._isNotNavigateToOtherPage = true;
      this._tenants = new ObservableArray<Tenant>([]);
      this.getTenantList();
    });
  }

  ngOnDestroy() {
    this._locationSub.unsubscribe();
  }

  /**
   * @description Retrieve list of tenants from server
   * @param onGetListFinished Optional parameter function trigger after retrieving list of tenants
   */
  getTenantList(onGetListFinished?: () => void) {
    setTimeout(() => {
      this._isBusy = true;
      this.tenantService
        .getTenants()
        .pipe(
          take(1),
          finalize(() => (this._isBusy = false))
        )
        .subscribe((tenants) => {
          // Store list of tenants from server
          this._tenants = new ObservableArray<Tenant>(tenants);

          // set flag indicating multiple tenant selection is not active
          this._multipleSelectionActive = false;

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
      selectedTenant.isSelected = true;
    }
  }

  /**
   * @description Navigates to add tenant details page
   */
  goToAddTenantsPage() {
    this.router.navigate(['/tenants/addTenant'], {
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
    const selectedTenants = this.tenantListView.listView.getSelectedItems();
    if (selectedTenants.length > 0) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TENANTS_LIST_PAGE.REMOVE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TENANTS_LIST_PAGE.REMOVE_DIALOG.MESSAGE'
          )
        )
        .then((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            setTimeout(() => {
              this._tenants = new ObservableArray<Tenant>([]);
              this.getTenantList();
            }, 1000);
          }
        });
    }
  }

  /**
   * @description Delete single selected tenant
   */
  deleteSelectedTenant() {
    console.log(this._currentTenant);
    this.tenantListView.listView.notifySwipeToExecuteFinished();
  }

  /**
   * @description Activates selected tenant
   */
  activateSelectedTenant() {
    console.log(this._currentTenant);
    this.tenantListView.listView.notifySwipeToExecuteFinished();
  }

  /**
   * @description Deactivates selected tenant
   */
  deactivateSelectedTenant() {
    console.log(this._currentTenant);
    this.tenantListView.listView.notifySwipeToExecuteFinished();
  }

  /**
   * @description RadListView.itemSwipeProgressStarted event handler. Configures swipe item threshold.
   * @param args ListViewEventData object
   */
  onSwipeCellStarted(args: ListViewEventData) {
    this._currentTenant = (<SwipeActionsEventData>args).mainView
      .bindingContext as Tenant;
    this._isCurrentTenantActive = this._currentTenant.isActive;
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
    this.getTenantList(() => {
      args.object.notifyPullToRefreshFinished();
    });
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
    } Selected Tenants`;
  }

  get isCurrentTenantActive(): boolean {
    return this._isCurrentTenantActive;
  }

  get isNotNavigateToOtherPage(): boolean {
    return this._isNotNavigateToOtherPage;
  }
}
