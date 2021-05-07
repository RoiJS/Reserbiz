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

import { delay, finalize } from 'rxjs/operators';

import { TranslateService } from '@ngx-translate/core';
import {
  RouterExtensions,
  ModalDialogOptions,
  ModalDialogService,
} from '@nativescript/angular';

import { AddContractAccountStatementDialogComponent } from '../add-contract-account-statement-dialog/add-contract-account-statement-dialog.component';
import { BaseListComponent } from '@src/app/shared/component/base-list.component';
import { ContractAccountStatementFilterDialogComponent } from '../contract-account-statement-filter-dialog/contract-account-statement-filter-dialog.component';

import { AccountStatement } from '@src/app/_models/account-statement.model';
import { AccountStatementFilter } from '@src/app/_models/filters/account-statement-filter.model';
import { AccountStatementPaginationList } from '@src/app/_models/pagination_list/account-statement-pagination-list.model';

import { AccountStatementService } from '@src/app/_services/account-statement.service';
import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { StorageService } from '@src/app/_services/storage.service';

import { NumberFormatter } from '@src/app/_helpers/formatters/number-formatter.helper';

import { PaymentStatusEnum } from '@src/app/_enum/payment-status.enum';
import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

import { IBaseListComponent } from '@src/app/_interfaces/components/ibase-list-component.interface';

@Component({
  selector: 'ns-contract-account-statement-list-panel',
  templateUrl: './contract-account-statement-list-panel.component.html',
  styleUrls: ['./contract-account-statement-list-panel.component.scss'],
})
export class ContractAccountStatementListPanelComponent
  extends BaseListComponent<AccountStatement>
  implements OnInit, OnChanges {
  @Input() currentContractId: number;
  @Input() IsCurrentContractArchived: boolean;
  @Input() IsCurrentContractEncashedDepositAmount: boolean;

  private _totalExpectedAmount = 0;
  private _totalPaidAmount = 0;
  private _totalExpectedDepositAmount = 0;
  private _totalPaidAmountFromDeposit = 0;
  private _totalEncashedDepositedAmount = 0;

  private ACCOUNT_STATMENT_FILTER_FROM_DATE = 'AccountStatementFilter_fromDate';
  private ACCOUNT_STATMENT_FILTER_TO_DATE = 'AccountStatementFilter_toDate';
  private ACCOUNT_STATMENT_FILTER_PAYMENT_STATUS =
    'AccountStatementFilter_paymentStatus';
  private ACCOUNT_STATMENT_FILTER_SORT_ORDER =
    'AccountStatementFilter_sortOrder';

  constructor(
    protected accountStatementService: AccountStatementService,
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

    this._entityFilter = new AccountStatementFilter();
    this._entityFilter.page = 1;
  }

  ngOnChanges(args: SimpleChanges) {
    if (args.currentContractId.currentValue) {
      this._entityFilter.parentId = +args.currentContractId.currentValue;
      this._loadListFlagSub = this.accountStatementService.loadAccountStatementListFlag.subscribe(
        (reset: boolean) => {
          this.initFilterOptions();
          this.getPaginatedEntities((e: AccountStatementPaginationList) => {
            this._totalExpectedAmount = e.totalExpectedAmount;
            this._totalPaidAmount = e.totalPaidAmount;

            this._totalExpectedDepositAmount = e.totalExpectedDepositAmount;
            this._totalPaidAmountFromDeposit = e.totalPaidAmountFromDeposit;
            this._totalEncashedDepositedAmount = e.totalEncashedDepositedAmount;
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
      `${this.ACCOUNT_STATMENT_FILTER_FROM_DATE}_${contractId}`
    );
    const toDate = this.storageService.getString(
      `${this.ACCOUNT_STATMENT_FILTER_TO_DATE}_${contractId}`
    );
    const paymentStatus = this.storageService.getString(
      `${this.ACCOUNT_STATMENT_FILTER_PAYMENT_STATUS}_${contractId}`
    );
    const sortOrder = this.storageService.getString(
      `${this.ACCOUNT_STATMENT_FILTER_SORT_ORDER}_${contractId}`
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
    const sortOrder = accountStatementFilter.sortOrder;
    const contractId = this._entityFilter.parentId;

    if (fromDate) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATMENT_FILTER_FROM_DATE}_${contractId}`,
        fromDate.toString()
      );
    }

    if (toDate) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATMENT_FILTER_TO_DATE}_${contractId}`,
        toDate.toString()
      );
    }

    if (paymentStatus) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATMENT_FILTER_PAYMENT_STATUS}_${contractId}`,
        paymentStatus.toString()
      );
    }

    if (sortOrder) {
      this.storageService.storeString(
        `${this.ACCOUNT_STATMENT_FILTER_SORT_ORDER}_${contractId}`,
        sortOrder.toString()
      );
    }
  }

  resetFilterOptions() {
    const contractId = this._entityFilter.parentId;

    this.storageService.remove(
      `${this.ACCOUNT_STATMENT_FILTER_FROM_DATE}_${contractId}`
    );
    this.storageService.remove(
      `${this.ACCOUNT_STATMENT_FILTER_TO_DATE}_${contractId}`
    );
    this.storageService.remove(
      `${this.ACCOUNT_STATMENT_FILTER_PAYMENT_STATUS}_${contractId}`
    );
    this.storageService.remove(
      `${this.ACCOUNT_STATMENT_FILTER_SORT_ORDER}_${contractId}`
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

  initAddAccountStatementDialog(
    suggestedAccountStatement: AccountStatement
  ): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        suggestedAccountStatement: suggestedAccountStatement,
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

  openAddAccountStatementDialog() {
    (async () => {
      const suggestedAccountStatement = await this.accountStatementService.getSuggestedNewAccountStatement(
        this._entityFilter.parentId
      );
      this.initAddAccountStatementDialog(suggestedAccountStatement).then(
        (data: { confirm: boolean; markAsPaid: boolean }) => {
          if (!data) {
            return;
          }

          if (data.confirm) {
            this.dialogService
              .confirm(
                this.translateService.instant(
                  'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE'
                ),
                this.translateService.instant(
                  'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.CONFIRM_MESSAGE'
                )
              )
              .then((res: ButtonOptions) => {
                if (res === ButtonOptions.YES) {
                  this._isBusy = true;

                  this.accountStatementService
                    .createNewAccountStatement(
                      this._entityFilter.parentId,
                      data.markAsPaid
                    )
                    .pipe(finalize(() => (this._isBusy = false)))
                    .subscribe(
                      () => {
                        this.dialogService.alert(
                          this.translateService.instant(
                            'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE'
                          ),
                          this.translateService.instant(
                            'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.SUCCESS_MESSAGE'
                          ),
                          () => {
                            // Needs to reset the page number to get
                            // the correct data
                            this._entityFilter.page = 1;
                            this.entityService.reloadListFlag();
                            this.contractService.reloadListFlag();
                          }
                        );
                      },
                      (error: Error) => {
                        this.dialogService.alert(
                          this.translateService.instant(
                            'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE'
                          ),
                          this.translateService.instant(
                            'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.ERROR_MESSAGE'
                          )
                        );
                      }
                    );
                } else {
                  this.openAddAccountStatementDialog();
                }
              });
          }
        }
      );
    })();
  }

  selectItem(currentIndex: number, url: string) {
    const selectedItem = <AccountStatement>(
      this._listItems.getItem(currentIndex)
    );

    this._isNotNavigateToOtherPage = false;

    this.navigateToOtherPage(
      `contracts/${this.currentContractId}/account-statement/${selectedItem.id}`
    );
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
