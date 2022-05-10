import { Location } from '@angular/common';
import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
  ChangeDetectorRef,
  NgZone,
  ViewContainerRef,
} from '@angular/core';

import { ActivatedRoute } from '@angular/router';

import { TranslateService } from '@ngx-translate/core';
import {
  RouterExtensions,
  ModalDialogOptions,
  ModalDialogService,
} from '@nativescript/angular';

import { AddContractAccountStatementDialogComponent } from '../add-contract-account-statement-dialog/add-contract-account-statement-dialog.component';
import { BaseListComponent } from '../../../shared/component/base-list.component';
import { ContractAccountStatementFilterDialogComponent } from '../contract-account-statement-filter-dialog/contract-account-statement-filter-dialog.component';

import { AccountStatement } from '../../../_models/account-statement.model';
import { AccountStatementFilter } from '../../../_models/filters/account-statement-filter.model';
import { AccountStatementPaginationList } from '../../../_models/pagination_list/account-statement-pagination-list.model';

import { AccountStatementService } from '../../../_services/account-statement.service';
import { ContractService } from '../../../_services/contract.service';
import { DialogService } from '../../../_services/dialog.service';
import { StorageService } from '../../../_services/storage.service';

import { NumberFormatter } from '../../../_helpers/formatters/number-formatter.helper';

import { AccountStatementTypeEnum } from '../../../_enum/account-statement-type.enum';
import { PaymentStatusEnum } from '../../../_enum/payment-status.enum';
import { SortOrderEnum } from '../../../_enum/sort-order.enum';

@Component({
  selector: 'ns-contract-account-statement-list-panel',
  templateUrl: './contract-account-statement-list-panel.component.html',
  styleUrls: ['./contract-account-statement-list-panel.component.scss'],
})
export class ContractAccountStatementListPanelComponent
  extends BaseListComponent<AccountStatement>
  implements OnInit, OnChanges
{
  @Input() currentContractId: number;
  @Input() IsCurrentContractArchived: boolean;
  @Input() IsCurrentContractEncashedDepositAmount: boolean;

  private _totalExpectedAmount = 0;
  private _totalPaidAmount = 0;
  private _totalExpectedDepositAmount = 0;
  private _totalPaidAmountFromDeposit = 0;
  private _totalEncashedDepositedAmount = 0;

  private ACCOUNT_STATEMENT_FILTER_FROM_DATE =
    'AccountStatementFilter_fromDate';
  private ACCOUNT_STATEMENT_FILTER_TO_DATE = 'AccountStatementFilter_toDate';
  private ACCOUNT_STATEMENT_FILTER_PAYMENT_STATUS =
    'AccountStatementFilter_paymentStatus';
  private ACCOUNT_STATEMENT_FILTER_ACCOUNT_STATEMENT_TYPE =
    'AccountStatementFilter_accountStatementType';
  private ACCOUNT_STATEMENT_FILTER_SORT_ORDER =
    'AccountStatementFilter_sortOrder';

  constructor(
    protected accountStatementService: AccountStatementService,
    protected activatedRoute: ActivatedRoute,
    protected contractService: ContractService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private modalDialogService: ModalDialogService,
    private storageService: StorageService,
    private vcRef: ViewContainerRef
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = accountStatementService;
    this.changeDetectorRef = changeDetectorRef;

    this.activatedRoute = activatedRoute;
    this._entityFilter = new AccountStatementFilter();
    this._entityFilter.page = 1;
  }

  ngOnChanges(args: SimpleChanges) {
    if (args.currentContractId.currentValue) {
      this._entityFilter.parentId = +args.currentContractId.currentValue;
      this._loadListFlagSub =
        this.accountStatementService.loadAccountStatementListFlag.subscribe(
          (reset: boolean) => {
            this.initFilterOptions();
            this.getPaginatedEntities((e: AccountStatementPaginationList) => {
              this._totalExpectedAmount = e.totalExpectedAmount;
              this._totalPaidAmount = e.totalPaidAmount;

              this._totalExpectedDepositAmount = e.totalExpectedDepositAmount;
              this._totalPaidAmountFromDeposit = e.totalPaidAmountFromDeposit;
              this._totalEncashedDepositedAmount =
                e.totalEncashedDepositedAmount;
            });
          }
        );
    }
  }

  ngOnInit() {
    super.ngOnInit();
  }

  initFilterOptions() {
    const contractId = this._entityFilter.parentId;

    const fromDate = this.storageService.getString(
      `${this.ACCOUNT_STATEMENT_FILTER_FROM_DATE}_${contractId}`
    );
    const toDate = this.storageService.getString(
      `${this.ACCOUNT_STATEMENT_FILTER_TO_DATE}_${contractId}`
    );
    const paymentStatus = this.storageService.getString(
      `${this.ACCOUNT_STATEMENT_FILTER_PAYMENT_STATUS}_${contractId}`
    );
    const accountStatementType = this.storageService.getString(
      `${this.ACCOUNT_STATEMENT_FILTER_ACCOUNT_STATEMENT_TYPE}_${contractId}`
    );
    const sortOrder = this.storageService.getString(
      `${this.ACCOUNT_STATEMENT_FILTER_SORT_ORDER}_${contractId}`
    );

    if (fromDate) {
      (<AccountStatementFilter>this._entityFilter).fromDate = new Date(
        fromDate
      );
    }

    if (toDate) {
      (<AccountStatementFilter>this._entityFilter).toDate = new Date(toDate);
    }

    if (paymentStatus) {
      (<AccountStatementFilter>this._entityFilter).paymentStatus = <
        PaymentStatusEnum
      >parseInt(paymentStatus);
    }

    if (accountStatementType) {
      (<AccountStatementFilter>this._entityFilter).accountStatementType = <
        AccountStatementTypeEnum
      >parseInt(accountStatementType);
    }

    if (sortOrder) {
      (<AccountStatementFilter>this._entityFilter).sortOrder = <SortOrderEnum>(
        parseInt(sortOrder)
      );
    }
  }

  storeFilterOptions(accountStatementFilter: AccountStatementFilter) {
    const fromDate = accountStatementFilter.fromDate;
    const toDate = accountStatementFilter.toDate;
    const paymentStatus = accountStatementFilter.paymentStatus;
    const accountStatementType = accountStatementFilter.accountStatementType;
    const sortOrder = accountStatementFilter.sortOrder;
    const contractId = this._entityFilter.parentId;

    if (fromDate) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATEMENT_FILTER_FROM_DATE}_${contractId}`,
        fromDate.toString()
      );
    }

    if (toDate) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATEMENT_FILTER_TO_DATE}_${contractId}`,
        toDate.toString()
      );
    }

    if (paymentStatus) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATEMENT_FILTER_PAYMENT_STATUS}_${contractId}`,
        paymentStatus.toString()
      );
    }

    if (accountStatementType) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATEMENT_FILTER_ACCOUNT_STATEMENT_TYPE}_${contractId}`,
        accountStatementType.toString()
      );
    }

    if (sortOrder) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATEMENT_FILTER_SORT_ORDER}_${contractId}`,
        sortOrder.toString()
      );
    }
  }

  resetFilterOptions() {
    const contractId = this._entityFilter.parentId;

    this.storageService.remove(
      `${this.ACCOUNT_STATEMENT_FILTER_FROM_DATE}_${contractId}`
    );
    this.storageService.remove(
      `${this.ACCOUNT_STATEMENT_FILTER_TO_DATE}_${contractId}`
    );
    this.storageService.remove(
      `${this.ACCOUNT_STATEMENT_FILTER_PAYMENT_STATUS}_${contractId}`
    );
    this.storageService.remove(
      `${this.ACCOUNT_STATEMENT_FILTER_ACCOUNT_STATEMENT_TYPE}_${contractId}`
    );
    this.storageService.remove(
      `${this.ACCOUNT_STATEMENT_FILTER_SORT_ORDER}_${contractId}`
    );
  }

  initFilterDialog(): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        accountStatementFilter: <AccountStatementFilter>this._entityFilter,
      },
      fullscreen: false,
      animated: true,
    };

    return this.modalDialogService.showModal(
      ContractAccountStatementFilterDialogComponent,
      dialogOptions
    );
  }

  initCreateNewAccountStatementDialog(contractId: number): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        contractId: contractId,
        accountStatementListCount: this._totalNumberOfItems,
      },
      fullscreen: false,
      animated: true,
    };

    return this.modalDialogService.showModal(
      AddContractAccountStatementDialogComponent,
      dialogOptions
    );
  }

  openFilterDialog() {
    this.initFilterDialog().then(
      (data: { filterHasChanged: boolean; filter: AccountStatementFilter }) => {
        if (!data) {
          return;
        }

        if (!data.filterHasChanged) {
          return;
        }

        if (data.filter.isFilterActive()) {
          this.storeFilterOptions(data.filter);
        } else {
          (<AccountStatementFilter>this._entityFilter).reset();
          this.resetFilterOptions();
        }

        // Needs to reset the page number to get
        // the correct data
        this._entityFilter.page = 1;
        this.entityService.reloadListFlag();
      }
    );
  }

  openCreateNewAccountStatementDialog() {
    (async () => {
      this.initCreateNewAccountStatementDialog(
        this._entityFilter.parentId
      ).then((data: { url: string }) => {
        if (!data) {
          return;
        }

        this.navigateToOtherPage(data.url);
      });
    })();
  }

  selectItem(currentIndex: number, url: string) {
    const selectedItem = <AccountStatement>(
      this._listItems.getItem(currentIndex)
    );

    this._isNotNavigateToOtherPage = false;

    this.navigateToOtherPage(`account-statement/${selectedItem.id}`);
  }

  get totalPaidAmount(): string {
    return NumberFormatter.formatCurrency(this._totalPaidAmount);
  }

  get totalExpectedAmount(): string {
    return NumberFormatter.formatCurrency(this._totalExpectedAmount);
  }

  get totalPaidAmountFromDeposit(): string {
    return NumberFormatter.formatCurrency(this._totalPaidAmountFromDeposit);
  }

  get totalExpectedDepositAmount(): string {
    return NumberFormatter.formatCurrency(this._totalExpectedDepositAmount);
  }

  get totalEncashedDepositedAmount(): string {
    return NumberFormatter.formatCurrency(this._totalEncashedDepositedAmount);
  }
}
