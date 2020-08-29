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
import { RouterExtensions } from 'nativescript-angular/router';
import {
  ModalDialogService,
  ModalDialogOptions,
} from 'nativescript-angular/modal-dialog';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { Contract } from '@src/app/_models/contract.model';
import { IBaseListComponent } from '@src/app/_interfaces/ibase-list-component.interface';
import { ContractFilterDialogComponent } from '../../contract-filter-dialog/contract-filter-dialog.component';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { StorageService } from '@src/app/_services/storage.service';
import { ContractPaginationList } from '@src/app/_models/contract-pagination-list.model';
import { ContractFilter } from '@src/app/_models/contract-filter.model';
import {
  ListViewEventData,
  SwipeActionsEventData,
} from 'nativescript-ui-listview';

@Component({
  selector: 'ns-contract-archived-list',
  templateUrl: './contract-archived-list.component.html',
  styleUrls: ['./contract-archived-list.component.scss'],
})
export class ContractArchivedListComponent extends BaseListComponent<Contract>
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
    private modalDialogService: ModalDialogService,
    private vcRef: ViewContainerRef,
    private storageService: StorageService
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
    this._deactivateMultipleItemDialogTexts = {
      title: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACTS_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACTS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACTS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACTS_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deactivateItemDialogTexts = {
      title: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.ERROR_MESSAGE'
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
