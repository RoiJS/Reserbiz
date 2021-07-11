import { Location } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  NgZone,
  OnDestroy,
  OnInit,
  ViewContainerRef,
} from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import {
  ModalDialogOptions,
  ModalDialogService,
  PageRoute,
  RouterExtensions,
} from '@nativescript/angular';

import { delay, finalize } from 'rxjs/operators';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';
import { PaymentDetailsDialogComponent } from './payment-details-dialog/payment-details-dialog.component';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { PaymentsService } from '@src/app/_services/payments.service';
import { AccountStatementService } from '@src/app/_services/account-statement.service';

import { AccountStatement } from '@src/app/_models/account-statement.model';
import { Contract } from '@src/app/_models/contract.model';
import { Payment } from '@src/app/_models/payment.model';
import { PaymentFilter } from '@src/app/_models/filters/payment-filter.model';
import { PaymentPaginationList } from '@src/app/_models/pagination_list/payment-pagination-list.model';

import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';
import { DialogIntentEnum } from '@src/app/_enum/dialog-intent.enum';
import { PaymentDto } from '@src/app/_dtos/payment-dto';
import { NumberFormatter } from '@src/app/_helpers/formatters/number-formatter.helper';

@Component({
  selector: 'ns-payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.scss'],
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

  private _firstAccountStatement: AccountStatement;
  private _contract: Contract;

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
    private pageRoute: PageRoute
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
        this._currentItemParentId = +paramMap.get('accountStatementId');
        const contractId = +paramMap.get('contractId');

        (async () => {
          this._firstAccountStatement =
            await this.accountStatementService.getFirstAccountStatement(
              contractId
            );

          this._contract = await this.contractService.getContract(contractId);

          this._loadListFlagSub =
            this.paymentService.loadPaymentListFlag.subscribe(
              (reset: boolean) => {
                (<PaymentFilter>this._entityFilter).contractId = contractId;
                (<PaymentFilter>this._entityFilter).parentId =
                  this._currentItemParentId;
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

  openAddPaymentDialog() {
    const newPaymentDetails = new Payment();

    this.initFilterDialog(newPaymentDetails, DialogIntentEnum.Add).then(
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
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'PAYMENT_DETAILS_DIALOG.ADD_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'PAYMENT_DETAILS_DIALOG.ADD_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.entityService.reloadListFlag();
                    this.accountStatementService.reloadListFlag(true);
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'PAYMENT_DETAILS_DIALOG.ADD_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'PAYMENT_DETAILS_DIALOG.ADD_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
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

    this.initFilterDialog(paymentDetails, DialogIntentEnum.View);
  }

  setSortOrder(sortOrder: SortOrderEnum) {
    this._entityFilter.sortOrder = <SortOrderEnum>sortOrder;
    this.paymentService.loadPaymentListFlag.next(true);
  }

  private initFilterDialog(
    paymentDetails: Payment,
    dialogIntent: DialogIntentEnum
  ): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        paymentDetails,
        dialogIntent,
        contract: this._contract,
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
