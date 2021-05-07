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
  RouterExtensions,
  ModalDialogService,
  ModalDialogOptions,
} from '@nativescript/angular';

import { delay } from 'rxjs/operators';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { Contract } from '@src/app/_models/contract.model';
import { IBaseListComponent } from '@src/app/_interfaces/components/ibase-list-component.interface';
import { ContractFilterDialogComponent } from '../contract-filter-dialog/contract-filter-dialog.component';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { StorageService } from '@src/app/_services/storage.service';
import { ContractPaginationList } from '@src/app/_models/pagination_list/contract-pagination-list.model';
import { ContractFilter } from '@src/app/_models/filters/contract-filter.model';
import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';

@Component({
  selector: 'ns-contract-list',
  templateUrl: './contract-list.component.html',
  styleUrls: ['./contract-list.component.scss'],
})
export class ContractListComponent
  extends BaseListComponent<Contract>
  implements IBaseListComponent, OnInit, OnDestroy {
  private _openContractsCount: number;

  private CONTRACT_FILTER_TENANT_ID = 'contractFilter_tenantId';
  private CONTRACT_FILTER_ACTIVE_FROM = 'contractFilter_activeFrom';
  private CONTRACT_FILTER_ACTIVE_TO = 'contractFilter_activeTo';
  private CONTRACT_FILTER_NEXT_DUE_DATE_FROM = 'contractFilter_nextDueDateFrom';
  private CONTRACT_FILTER_NEXT_DUE_DATE_TO = 'contractFilter_nextDueDateTo';
  private CONTRACT_FILTER_OPEN_CONTRACT = 'contractFilter_openContract';
  private CONTRACT_FILTER_SORT_ORDER = 'contractFilter_sortOrder';

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
    this._openContractsCount = 0;

    // take note to override the filter from the
    // base component list if derived component
    // class need to support more complex
    // filter feature.
    this._entityFilter = new ContractFilter();
    this._entityFilter.page = 1;
  }

  ngOnInit() {
    this._loadListFlagSub = this.contractService.loadContractListFlag.subscribe(
      () => {
        this.initFilterOptions();
        this.getPaginatedEntities(
          (contractPaginationList: ContractPaginationList) => {
            this._openContractsCount =
              contractPaginationList.totalNumberOfOpenContracts;
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

  initFilterOptions() {
    const tenantId = this.storageService.getString(
      this.CONTRACT_FILTER_TENANT_ID
    );
    const activeFrom = this.storageService.getString(
      this.CONTRACT_FILTER_ACTIVE_FROM
    );
    const activeTo = this.storageService.getString(
      this.CONTRACT_FILTER_ACTIVE_TO
    );
    const nextDueDateFrom = this.storageService.getString(
      this.CONTRACT_FILTER_NEXT_DUE_DATE_FROM
    );
    const nextDueDateTo = this.storageService.getString(
      this.CONTRACT_FILTER_NEXT_DUE_DATE_TO
    );
    const openContract = this.storageService.getString(
      this.CONTRACT_FILTER_OPEN_CONTRACT
    );
    const sortOrder = this.storageService.getString(
      this.CONTRACT_FILTER_SORT_ORDER
    );

    if (tenantId) {
      (<ContractFilter>this._entityFilter).tenantId = Number(tenantId);
    }

    if (activeFrom) {
      (<ContractFilter>this._entityFilter).activeFromFilter = new Date(
        activeFrom
      );
    }

    if (activeTo) {
      (<ContractFilter>this._entityFilter).activeToFilter = new Date(activeTo);
    }

    if (nextDueDateFrom) {
      (<ContractFilter>this._entityFilter).nextDueDateFromFilter = new Date(
        nextDueDateFrom
      );
    }

    if (nextDueDateTo) {
      (<ContractFilter>this._entityFilter).nextDueDateToFilter = new Date(
        nextDueDateTo
      );
    }

    if (openContract) {
      (<ContractFilter>this._entityFilter).openContract = Boolean(
        JSON.parse(openContract)
      );
    }

    if (sortOrder) {
      (<ContractFilter>this._entityFilter).sortOrder = <SortOrderEnum>(
        parseInt(sortOrder)
      );
    }
  }

  storeFilterOptions(contractFilter: ContractFilter) {
    const tenantId = contractFilter.tenantId;
    const activeFrom = contractFilter.activeFromFilter;
    const activeTo = contractFilter.activeToFilter;
    const nextDueDateFrom = contractFilter.nextDueDateFromFilter;
    const nextDueDateTo = contractFilter.nextDueDateToFilter;
    const openContract = contractFilter.openContract;
    const sortOrder = contractFilter.sortOrder;

    if (tenantId) {
      this.storageService.storeString(
        this.CONTRACT_FILTER_TENANT_ID,
        tenantId.toString()
      );
    }

    if (activeFrom) {
      this.storageService.storeString(
        this.CONTRACT_FILTER_ACTIVE_FROM,
        activeFrom.toString()
      );
    }

    if (activeTo) {
      this.storageService.storeString(
        this.CONTRACT_FILTER_ACTIVE_TO,
        activeTo.toString()
      );
    }

    if (nextDueDateFrom) {
      this.storageService.storeString(
        this.CONTRACT_FILTER_NEXT_DUE_DATE_FROM,
        nextDueDateFrom.toString()
      );
    }

    if (nextDueDateTo) {
      this.storageService.storeString(
        this.CONTRACT_FILTER_NEXT_DUE_DATE_TO,
        nextDueDateTo.toString()
      );
    }

    if (openContract) {
      this.storageService.storeString(
        this.CONTRACT_FILTER_OPEN_CONTRACT,
        openContract.toString()
      );
    }

    if (sortOrder) {
      this.storageService.storeString(
        this.CONTRACT_FILTER_SORT_ORDER,
        sortOrder.toString()
      );
    }
  }

  resetFilterOptions() {
    this.storageService.remove(this.CONTRACT_FILTER_TENANT_ID);
    this.storageService.remove(this.CONTRACT_FILTER_ACTIVE_FROM);
    this.storageService.remove(this.CONTRACT_FILTER_ACTIVE_TO);
    this.storageService.remove(this.CONTRACT_FILTER_NEXT_DUE_DATE_FROM);
    this.storageService.remove(this.CONTRACT_FILTER_NEXT_DUE_DATE_TO);
    this.storageService.remove(this.CONTRACT_FILTER_OPEN_CONTRACT);
    this.storageService.remove(this.CONTRACT_FILTER_SORT_ORDER);
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
      ContractFilterDialogComponent,
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

  get openContractsCount(): number {
    return this._openContractsCount;
  }
}
