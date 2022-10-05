import { Location } from "@angular/common";
import {
  ChangeDetectorRef,
  Component,
  NgZone,
  OnDestroy,
  OnInit,
  ViewContainerRef,
} from "@angular/core";

import { TranslateService } from "@ngx-translate/core";

import {
  ModalDialogOptions,
  ModalDialogService,
  PageRoute,
  RouterExtensions,
} from "@nativescript/angular";

import { finalize } from "rxjs/operators";

import { BaseListComponent } from "~/app/shared/component/base-list.component";
import { PaymentDetailsDialogComponent } from "./payment-details-dialog/payment-details-dialog.component";
import { PaymentFilterDialogComponent } from "./payment-filter-dialog/payment-filter-dialog.component";

import { AccountStatementService } from "~/app/_services/account-statement.service";
import { ContractService } from "~/app/_services/contract.service";
import { DialogService } from "~/app/_services/dialog.service";
import { PaymentsService } from "~/app/_services/payments.service";
import { StorageService } from "~/app/_services/storage.service";

import { AccountStatement } from "~/app/_models/account-statement.model";
import { Contract } from "~/app/_models/contract.model";
import { Payment } from "~/app/_models/payment.model";
import { PaymentFilter } from "~/app/_models/filters/payment-filter.model";
import { PaymentPaginationList } from "~/app/_models/pagination_list/payment-pagination-list.model";

import { AccountStatementTypeEnum } from "~/app/_enum/account-statement-type.enum";
import { SortOrderEnum } from "~/app/_enum/sort-order.enum";
import { DialogIntentEnum } from "~/app/_enum/dialog-intent.enum";
import { PaymentDto } from "~/app/_dtos/payment-dto";
import { PaymentForTypeEnum } from "~/app/_enum/payment-type.enum";

import { NumberFormatter } from "~/app/_helpers/formatters/number-formatter.helper";

@Component({
  selector: "ns-payments",
  templateUrl: "./payments.component.html",
  styleUrls: ["./payments.component.scss"],
})
export class PaymentsComponent
  extends BaseListComponent<Payment>
  implements OnInit, OnDestroy
{
  private _totalPaidAmount = 0;
  private _suggestedRentalAmount = 0;
  private _suggestedElectricBillAmount = 0;
  private _suggestedWaterBillAmount = 0;
  private _suggestedMiscellaneousFeesAmount = 0;
  private _suggestedPenaltyAmount = 0;
  private _depositedAmountBalance = 0;
  private _totalAmountFromDeposit = 0;

  private _totalExpectedRentalAmount = 0;
  private _totalExpectedElectricBillAmount = 0;
  private _totalExpectedwaterBillAmount = 0;
  private _totalExpectedMiscellaneousFeesAmount = 0;
  private _totalExpectedPenaltyAmount = 0;

  private _totalPaidRentalAmount = 0;
  private _totalPaidElectricBillAmount = 0;
  private _totalPaidWaterBillAmount = 0;
  private _totalPaidMiscellaneousFeesAmount = 0;
  private _totalPaidPenaltyAmount = 0;

  private _accountStatementDetails: AccountStatement;
  private _firstAccountStatement: AccountStatement;
  private _contract: Contract;

  private PAYMENT_FILTER_PAYMENT_FOR_TYPE = "PaymentFilter_paymentForType";
  private PAYMENT_FILTER_SORT_ORDER = "PaymentFilter_sortOrder";

  constructor(
    protected paymentService: PaymentsService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private accountStatementService: AccountStatementService,
    private contractService: ContractService,
    private modalDialogService: ModalDialogService,
    private vcRef: ViewContainerRef,
    private pageRoute: PageRoute,
    private storageService: StorageService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = paymentService;
    this.changeDetectorRef = changeDetectorRef;

    // take note to override the filter from the
    // base component list if derived component
    // class need to support more complex
    // filter feature.
    this._entityFilter = new PaymentFilter();
    this._entityFilter.page = 1;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentItemParentId = +paramMap.get("accountStatementId");
        const contractId = +paramMap.get("contractId");

        (async () => {
          this._firstAccountStatement =
            await this.accountStatementService.getFirstAccountStatement(
              contractId
            );

          this._accountStatementDetails =
            await this.accountStatementService.getAccountStatement(
              this._currentItemParentId
            );

          this._contract = await this.contractService.getContract(contractId);

          this._loadListFlagSub =
            this.paymentService.loadPaymentListFlag.subscribe(
              (reset: boolean) => {
                (<PaymentFilter>this._entityFilter).contractId = contractId;
                (<PaymentFilter>this._entityFilter).parentId =
                  this._currentItemParentId;

                this.initFilterOptions();
                this.getPaginatedEntities(
                  (paymentPaginationList: PaymentPaginationList) => {
                    this._totalPaidAmount = paymentPaginationList.totalAmount;
                    this._totalAmountFromDeposit =
                      paymentPaginationList.totalAmountFromDeposit;

                    this._suggestedRentalAmount =
                      paymentPaginationList.suggestedRentalAmount;
                    this._suggestedElectricBillAmount =
                      paymentPaginationList.suggestedElectricBillAmount;
                    this._suggestedWaterBillAmount =
                      paymentPaginationList.suggestedWaterBillAmount;
                    this._suggestedMiscellaneousFeesAmount =
                      paymentPaginationList.suggestedMiscelleneousAmount;
                    this._suggestedPenaltyAmount =
                      paymentPaginationList.suggestedPenaltyAmount;
                    this._depositedAmountBalance =
                      paymentPaginationList.depositedAmountBalance;

                    this._totalExpectedRentalAmount =
                      paymentPaginationList.totalExpectedRentalAmount;
                    this._totalExpectedElectricBillAmount =
                      paymentPaginationList.totalExpectedElectricBillAmount;
                    this._totalExpectedwaterBillAmount =
                      paymentPaginationList.totalExpectedWaterBillAmount;
                    this._totalExpectedMiscellaneousFeesAmount =
                      paymentPaginationList.totalExpectedMiscellaneousFeesAmount;
                    this._totalExpectedPenaltyAmount =
                      paymentPaginationList.totalExpectedPenaltyAmount;

                    this._totalPaidRentalAmount =
                      paymentPaginationList.totalPaidRentalAmount;
                    this._totalPaidElectricBillAmount =
                      paymentPaginationList.totalPaidElectricBillAmount;
                    this._totalPaidWaterBillAmount =
                      paymentPaginationList.totalPaidWaterBillAmount;
                    this._totalPaidMiscellaneousFeesAmount =
                      paymentPaginationList.totalPaidMiscellaneousFeesAmount;
                    this._totalPaidPenaltyAmount =
                      paymentPaginationList.totalPaidPenaltyAmount;
                  }
                );
              }
            );
        })();
      });
    });

    super.ngOnInit();
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  //#region "Filter-related functions"
  openFilterDialog() {
    this.initFilterDialog().then(
      (data: { filterHasChanged: boolean; filter: PaymentFilter }) => {
        if (!data) {
          return;
        }

        if (!data.filterHasChanged) {
          return;
        }

        if (data.filter.isFilterActive()) {
          this.storeFilterOptions(data.filter);
        } else {
          (<PaymentFilter>this._entityFilter).reset();
          this.resetFilterOptions();
        }

        // Needs to reset the page number to get
        // the correct data
        this._entityFilter.page = 1;
        this.entityService.reloadListFlag();
      }
    );
  }

  private initFilterOptions() {
    const paymentForType = this.storageService.getString(
      `${this.PAYMENT_FILTER_PAYMENT_FOR_TYPE}_${this._currentItemParentId}`
    );
    const sortOrder = this.storageService.getString(
      `${this.PAYMENT_FILTER_SORT_ORDER}_${this._currentItemParentId}`
    );

    if (paymentForType) {
      (<PaymentFilter>this._entityFilter).paymentForType = <PaymentForTypeEnum>(
        parseInt(paymentForType)
      );
    }

    if (sortOrder) {
      (<PaymentFilter>this._entityFilter).sortOrder = <SortOrderEnum>(
        parseInt(sortOrder)
      );
    }
  }

  private storeFilterOptions(paymentFilter: PaymentFilter) {
    const paymentForType = paymentFilter.paymentForType;
    const sortOrder = paymentFilter.sortOrder;

    if (paymentForType) {
      this.storageService.storeString(
        `${this.PAYMENT_FILTER_PAYMENT_FOR_TYPE}_${this._currentItemParentId}`,
        paymentForType.toString()
      );
    }

    if (sortOrder) {
      this.storageService.storeString(
        `${this.PAYMENT_FILTER_SORT_ORDER}_${this._currentItemParentId}`,
        sortOrder.toString()
      );
    }
  }

  private resetFilterOptions() {
    this.storageService.remove(
      `${this.PAYMENT_FILTER_PAYMENT_FOR_TYPE}_${this._currentItemParentId}`
    );
    this.storageService.remove(
      `${this.PAYMENT_FILTER_SORT_ORDER}_${this._currentItemParentId}`
    );
  }

  private initFilterDialog(): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        accountStatementDetails: this._accountStatementDetails,
        paymentFilter: <PaymentFilter>this._entityFilter,
      },
      fullscreen: false,
      animated: true,
    };

    return this.modalDialogService.showModal(
      PaymentFilterDialogComponent,
      dialogOptions
    );
  }
  //#endregion

  //#region "Payment dialog related functions"
  openAddPaymentDialog() {
    const isAccountStatementForRentalBill =
      this._accountStatementDetails.accountStatementType ===
      AccountStatementTypeEnum.RentalBill;
    const isAccountStatementForUtilityBill =
      this._accountStatementDetails.accountStatementType ===
      AccountStatementTypeEnum.UtilityBilll;

    const newPaymentDetails = new Payment();

    if (isAccountStatementForRentalBill) {
      newPaymentDetails.paymentForType = PaymentForTypeEnum.Rental;
    } else if (isAccountStatementForUtilityBill) {
      newPaymentDetails.paymentForType = PaymentForTypeEnum.ElectricBill;
    }

    this.initPaymentDialog(newPaymentDetails, DialogIntentEnum.Add).then(
      (returnedPaymentDetails: PaymentDto) => {
        if (returnedPaymentDetails) {
          this._isBusy = true;

          this.paymentService
            .saveNewEntity({
              id: this._currentItemParentId,
              dtoEntity: returnedPaymentDetails,
            })
            .pipe(
              finalize(() => {
                this.ngZone.run(() => {
                  this._isBusy = false;
                });
              })
            )
            .subscribe({
              next: () => {
                this.dialogService
                  .alert(
                    this.translateService.instant(
                      "PAYMENT_DETAILS_DIALOG.ADD_DIALOG.TITLE"
                    ),
                    this.translateService.instant(
                      "PAYMENT_DETAILS_DIALOG.ADD_DIALOG.SUCCESS_MESSAGE"
                    )
                  )
                  .then(() => {
                    this.entityService.reloadListFlag();
                    this.accountStatementService.reloadListFlag(true);
                  });
              },
              error: (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    "PAYMENT_DETAILS_DIALOG.ADD_DIALOG.TITLE"
                  ),
                  this.translateService.instant(
                    "PAYMENT_DETAILS_DIALOG.ADD_DIALOG.ERROR_MESSAGE"
                  )
                );
              },
            });
        }
      }
    );
  }

  openPaymentDetailsDialog(paymentItemIndex: number) {
    const paymentDetails = <Payment>this._listItems.getItem(paymentItemIndex);

    setTimeout(() => {
      // Override default behavior when selecting item from the list
      this.appListView.listView.deselectItemAt(paymentItemIndex);
    }, 100);

    this.initPaymentDialog(paymentDetails, DialogIntentEnum.View);
  }

  private initPaymentDialog(
    paymentDetails: Payment,
    dialogIntent: DialogIntentEnum
  ): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        paymentDetails,
        dialogIntent,
        contract: this._contract,
        accountStatementDetails: this._accountStatementDetails,
        currentAccountStatementId: this._currentItemParentId,
        firstAccountStatement: this._firstAccountStatement,
        depositedAmountBalance: this._depositedAmountBalance,
        suggestedRentalAmount: this._suggestedRentalAmount,
        suggestedElectricBillAmount: this._suggestedElectricBillAmount,
        suggestedWaterBillAmount: this._suggestedWaterBillAmount,
        suggestedMiscellaneousFeesAmount:
          this._suggestedMiscellaneousFeesAmount,
        suggestedPenaltyAmount: this._suggestedPenaltyAmount,

        totalExpectedRentalAmount: this._totalExpectedRentalAmount,
        totalExpectedElectricBillAmount: this._totalExpectedElectricBillAmount,
        totalExpectedWaterBillAmount: this._totalExpectedwaterBillAmount,
        totalExpectedMiscellaneousFeesAmount:
          this._totalExpectedMiscellaneousFeesAmount,
        totalExpectedPenaltyAmount: this._totalExpectedPenaltyAmount,

        totalPaidRentalAmount: this._totalPaidRentalAmount,
        totalPaidElectricBillAmount: this._totalPaidElectricBillAmount,
        totalPaidWaterBillAmount: this._totalPaidWaterBillAmount,
        totalPaidMiscellaneousFeesAmount:
          this._totalPaidMiscellaneousFeesAmount,
        totalPaidPenaltyAmount: this._totalPaidPenaltyAmount,
      },
      fullscreen: false,
      animated: true,
    };

    return this.modalDialogService.showModal(
      PaymentDetailsDialogComponent,
      dialogOptions
    );
  }
  //#endregion

  get currentSortOrder(): SortOrderEnum {
    return this._entityFilter.sortOrder;
  }

  get totalPaidAmount(): string {
    return NumberFormatter.formatCurrency(this._totalPaidAmount);
  }

  get totalAmountFromDeposit(): string {
    return NumberFormatter.formatCurrency(this._totalAmountFromDeposit);
  }

  get hasTotalAmountFromDeposit(): boolean {
    return this._totalAmountFromDeposit > 0;
  }
}
