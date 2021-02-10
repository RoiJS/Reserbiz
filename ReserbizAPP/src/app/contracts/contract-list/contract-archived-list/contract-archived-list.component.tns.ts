import { Location } from '@angular/common';
import {
  Component,
  OnInit,
  OnDestroy,
  NgZone,
  ChangeDetectorRef,
} from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from '@nativescript/angular';

import {
  ListViewEventData,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { IBaseListComponent } from '@src/app/_interfaces/ibase-list-component.interface';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceService } from '@src/app/_services/space.service';

import { ContractPaginationList } from '@src/app/_models/contract-pagination-list.model';
import { ContractFilter } from '@src/app/_models/contract-filter.model';
import { Contract } from '@src/app/_models/contract.model';
import { Space } from '@src/app/_models/space.model';

@Component({
  selector: 'ns-contract-archived-list',
  templateUrl: './contract-archived-list.component.html',
  styleUrls: ['./contract-archived-list.component.scss'],
})
export class ContractArchivedListComponent
  extends BaseListComponent<Contract>
  implements IBaseListComponent, OnInit, OnDestroy {
  private _expiredContractsCount: number;
  private _inactiveContractsCount: number;
  private _isCurrentItemExpired = false;

  constructor(
    protected contractService: ContractService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private spaceService: SpaceService
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
    this._loadListFlagSub = this.contractService.loadContractListFlag.subscribe(
      () => {
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
      }
    );

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
