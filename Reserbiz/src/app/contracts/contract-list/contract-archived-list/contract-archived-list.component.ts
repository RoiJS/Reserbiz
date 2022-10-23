import { Location } from '@angular/common';
import {
  Component,
  OnInit,
  OnDestroy,
  NgZone,
  ChangeDetectorRef,
  ViewContainerRef,
} from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import {
  ModalDialogOptions,
  ModalDialogService,
  RouterExtensions,
} from '@nativescript/angular';

import {
  ListViewEventData,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';

import { delay } from 'rxjs/operators';

import { BaseListComponent } from '../../../shared/component/base-list.component';
import { ContractArchivedFilterDialogComponent } from './contract-archived-filter-dialog/contract-archived-filter-dialog.component';

import { IBaseListComponent } from '../../../_interfaces/components/ibase-list-component.interface';

import { ContractService } from '../../../_services/contract.service';
import { DialogService } from '../../../_services/dialog.service';
import { SpaceService } from '../../../_services/space.service';
import { StorageService } from '../../../_services/storage.service';

import { ContractPaginationList } from '../../../_models/pagination_list/contract-pagination-list.model';
import { ContractFilter } from '../../../_models/filters/contract-filter.model';
import { Contract } from '../../../_models/contract.model';
import { Space } from '../../../_models/space.model';

import { ArchivedContractStatusEnum } from '../../../_enum/archived-contract-options.enum';
import { SortOrderEnum } from '../../../_enum/sort-order.enum';

@Component({
  selector: 'ns-contract-archived-list',
  templateUrl: './contract-archived-list.component.html',
  styleUrls: ['./contract-archived-list.component.scss'],
})
export class ContractArchivedListComponent
  extends BaseListComponent<Contract>
  implements IBaseListComponent, OnInit, OnDestroy
{
  private _expiredContractsCount: number;
  private _inactiveContractsCount: number;
  private _isCurrentItemExpired = false;

  private ARCHIVED_CONTRACT_FILTER_STATUS = 'archviedContractFilter_status';
  private ARCHIVED_CONTRACT_FILTER_CODE_SORT_ORDER =
    'archivedContractFilter_codeSortOrder';

  constructor(
    protected contractService: ContractService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private modalDialogService: ModalDialogService,
    private spaceService: SpaceService,
    private storageService: StorageService,
    private vcRef: ViewContainerRef
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = contractService;
    this.changeDetectorRef = changeDetectorRef;
    this._expiredContractsCount = 0;

    // take note to override the filter from the
    // base component list if derived component
    // class need to support more complex
    // filter feature.
    this._entityFilter = new ContractFilter();
    (<ContractFilter>this._entityFilter).page = 1;
    (<ContractFilter>this._entityFilter).archived = true;
  }

  ngOnInit() {
    this._loadListFlagSub = this.contractService.loadContractListFlag
      .pipe(delay(1000))
      .subscribe(() => {
        this.initFilterOptions();
        this.getPaginatedEntities(
          (contractPaginationList: ContractPaginationList) => {
            this._expiredContractsCount =
              contractPaginationList.totalNumberOfExpiredContracts;
            this._inactiveContractsCount =
              contractPaginationList.totalNumberOfInactiveContracts;

            this._listItems.map((contract: Contract) => {
              contract.convertDurationBeforeContractEndsToString(
                this.translateService
              );
            });
          }
        );
      });

    this.initDialogTexts();
    super.ngOnInit();
  }

  initDialogTexts() {
    this._activateMultipleItemDialogTexts = {
      title: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACTS_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACTS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACTS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACTS_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._activateItemDialogTexts = {
      title: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'CONTRACT_ARCHIVED_LIST_PAGE.DEARCHIVE_CONTRACT_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  onSwipeCellStarted(args: ListViewEventData) {
    const contract = (<SwipeActionsEventData>args).mainView
      .bindingContext as Contract;
    this._isCurrentItemExpired = contract.isExpired;
    super.onSwipeCellStarted(args);
  }

  activateSelectedItem() {
    this.spaceService
      .getSpace(this._currentItem.spaceId)
      .subscribe((space: Space) => {
        const isSpaceStillAvailable =
          space.isNotOccupied &&
          space.occupiedByContractId !== this._currentItem.id;

        // Check the availability of space attached to the contract
        if (!isSpaceStillAvailable) {
          this.dialogService.alert(
            this.translateService.instant(
              'CONTRACT_ARCHIVED_LIST_PAGE.ARCHIVED_FAILED_DIALOG.TITLE'
            ),
            this.translateService.instant(
              'CONTRACT_ARCHIVED_LIST_PAGE.ARCHIVED_FAILED_DIALOG.ERROR_MESSAGE'
            )
          );
          return;
        }

        super.activateSelectedItem();
      });
  }

  initFilterOptions() {
    const status = this.storageService.getString(
      this.ARCHIVED_CONTRACT_FILTER_STATUS
    );
    const codeSortOrder = this.storageService.getString(
      this.ARCHIVED_CONTRACT_FILTER_CODE_SORT_ORDER
    );

    if (status) {
      (<ContractFilter>this._entityFilter).archivedContractStatus = <
        ArchivedContractStatusEnum
      >parseInt(status);
    }

    if (codeSortOrder) {
      (<ContractFilter>this._entityFilter).codeSortOrder = <SortOrderEnum>(
        parseInt(codeSortOrder)
      );
    }
  }

  storeFilterOptions(contractFilter: ContractFilter) {
    const status = contractFilter.archivedContractStatus;
    const codeSortOrder = contractFilter.sortOrder;

    if (status) {
      this.storageService.storeString(
        this.ARCHIVED_CONTRACT_FILTER_STATUS,
        status.toString()
      );
    }

    if (codeSortOrder) {
      this.storageService.storeString(
        this.ARCHIVED_CONTRACT_FILTER_CODE_SORT_ORDER,
        codeSortOrder.toString()
      );
    }
  }

  resetFilterOptions() {
    this.storageService.remove(this.ARCHIVED_CONTRACT_FILTER_STATUS);
    this.storageService.remove(this.ARCHIVED_CONTRACT_FILTER_CODE_SORT_ORDER);
  }

  initFilterDialog(): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        contractFilter: <ContractFilter>this._entityFilter,
      },
      fullscreen: false,
      animated: true,
    };

    return this.modalDialogService.showModal(
      ContractArchivedFilterDialogComponent,
      dialogOptions
    );
  }

  openFilterDialog() {
    this.initFilterDialog().then(
      (data: { filterHasChanged: boolean; filter: ContractFilter }) => {
        if (!data) {
          return;
        }

        if (!data.filterHasChanged) {
          return;
        }

        if (data.filter.isFilterActive()) {
          this.storeFilterOptions(data.filter);
        } else {
          (<ContractFilter>this._entityFilter).reset();
          this.resetFilterOptions();
        }

        // Needs to reset the page number to get
        // the correct data
        this._entityFilter.page = 1;
        this.entityService.reloadListFlag();
      }
    );
  }

  get expiredContractsCount(): number {
    return this._expiredContractsCount;
  }

  get inactiveContractsCount(): number {
    return this._inactiveContractsCount;
  }

  get isCurrentItemExpired(): boolean {
    return this._isCurrentItemExpired;
  }
}
