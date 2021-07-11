import { Component, OnInit, ViewChild } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { ModalDialogParams } from '@nativescript/angular';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';
import { DataFormEventData } from 'nativescript-ui-dataform';

import { AccountStatement } from '@src/app/_models/account-statement.model';
import { BaseFormHelper } from '@src/app/_helpers/base_helpers/base-form.helper';
import { NumberFormatter } from '@src/app/_helpers/formatters/number-formatter.helper';
import { PaymentMapper } from '@src/app/_helpers/mappers/payment-mapper.helper';

import { PaymentTypeValueProvider } from '@src/app/_helpers/value_providers/payment-type-value-provider.helper';

import { DialogService } from '@src/app/_services/dialog.service';

import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { DialogIntentEnum } from '@src/app/_enum/dialog-intent.enum';
import { PaymentForTypeEnum } from '@src/app/_enum/payment-type.enum';

import { Contract } from '@src/app/_models/contract.model';
import { Payment } from '@src/app/_models/payment.model';
import { PaymentFormSource } from '@src/app/_models/form/payment-form.model';

@Component({
  selector: 'ns-payment-details-dialog',
  templateUrl: './payment-details-dialog.component.html',
  styleUrls: ['./payment-details-dialog.component.scss'],
})
export class PaymentDetailsDialogComponent
  extends BaseFormHelper<PaymentFormSource>
  implements OnInit
{
  @ViewChild(RadDataFormComponent, { static: false })
  formSource: RadDataFormComponent;

  private _dialogTitle = '';
  private _dialogIntent: DialogIntentEnum;

  private _contract: Contract;
  private _suggestedRentalAmount = 0;
  private _suggestedElectricBillAmount = 0;
  private _suggestedWaterBillAmount = 0;
  private _suggestedMiscellaneousFeesAmount = 0;
  private _suggestedPenaltyAmount = 0;

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

  private _depositedAmountBalance = 0;
  private _currentAccountStatementId = 0;

  private _firstAccountStatement = new AccountStatement();

  private _paymentDetailsFormSource: PaymentFormSource;

  private _paymentTypeValueProvider: PaymentTypeValueProvider;

  constructor(
    private dialogService: DialogService,
    private params: ModalDialogParams,
    private translateService: TranslateService
  ) {
    super();
    this.setDialogTitle(params.context.dialogIntent);
    this.initFormDetails(params.context.paymentDetails);

    this._contract = params.context.contract;
    this._dialogIntent = params.context.dialogIntent;
    this._currentAccountStatementId = params.context.currentAccountStatementId;
    this._firstAccountStatement = params.context.firstAccountStatement;
    this._suggestedRentalAmount = params.context.suggestedRentalAmount;
    this._suggestedElectricBillAmount =
      params.context.suggestedElectricBillAmount;
    this._suggestedWaterBillAmount = params.context.suggestedWaterBillAmount;
    this._suggestedMiscellaneousFeesAmount =
      params.context.suggestedMiscellaneousFeesAmount;
    this._suggestedPenaltyAmount = params.context.suggestedPenaltyAmount;
    this._depositedAmountBalance = params.context.depositedAmountBalance;

    this._totalExpectedRentalAmount = params.context.totalExpectedRentalAmount;
    this._totalExpectedElectricBillAmount =
      params.context.totalExpectedElectricBillAmount;
    this._totalExpectedwaterBillAmount =
      params.context.totalExpectedWaterBillAmount;
    this._totalExpectedMiscellaneousFeesAmount =
      params.context.totalExpectedMiscellaneousFeesAmount;
    this._totalExpectedPenaltyAmount =
      params.context.totalExpectedPenaltyAmount;

    this._totalPaidRentalAmount = params.context.totalPaidRentalAmount;
    this._totalPaidElectricBillAmount =
      params.context.totalPaidElectricBillAmount;
    this._totalPaidWaterBillAmount = params.context.totalPaidWaterBillAmount;
    this._totalPaidMiscellaneousFeesAmount =
      params.context.totalPaidMiscellaneousFeesAmount;
    this._totalPaidPenaltyAmount = params.context.totalPaidPenaltyAmount;
  }

  ngOnInit() {
    this._paymentTypeValueProvider = new PaymentTypeValueProvider(
      this.translateService
    );
  }

  onSave() {
    const isFormValid = this.validateForm();

    if (isFormValid) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'PAYMENT_DETAILS_DIALOG.ADD_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'PAYMENT_DETAILS_DIALOG.ADD_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            const paymentCreateDto = this.paymentMapper.mapFormSourceToDto(
              this._paymentDetailsFormSource
            );
            this.params.closeCallback(paymentCreateDto);
          }
        });
    }
  }

  onClose() {
    this.params.closeCallback();
  }

  onEditorUpdate(args: DataFormEventData) {
    if (args.propertyName === 'dateReceived') {
      if (typeof args.editor.setDateFormat !== 'undefined') {
        this.changeDateFormatting(args.editor);
      }
    }
  }

  onPropertyCommitted(args: DataFormEventData) {
    /**
     * If Amount From Deposit, pre-fill the amount field
     */
    if (
      args.propertyName === 'isAmountFromDeposit' ||
      args.propertyName === 'paymentForType'
    ) {
      if (
        this._paymentDetailsFormSource.isAmountFromDeposit &&
        (!this._paymentDetailsFormSource.amount ||
          this._paymentDetailsFormSource.amount === 0)
      ) {
        let suggestedAmount = 0;

        switch (this._paymentDetailsFormSource.paymentForType) {
          case PaymentForTypeEnum.Rental:
            suggestedAmount = this._suggestedRentalAmount;
            break;
          case PaymentForTypeEnum.ElectricBill:
            suggestedAmount = this._suggestedElectricBillAmount;
            break;
          case PaymentForTypeEnum.WaterBill:
            suggestedAmount = this._suggestedWaterBillAmount;
            break;
          case PaymentForTypeEnum.MiscellaneousFee:
            suggestedAmount = this._suggestedMiscellaneousFeesAmount;
            break;
          case PaymentForTypeEnum.Penalty:
            suggestedAmount = this._suggestedPenaltyAmount;
            break;
          default:
            break;
        }

        this._paymentDetailsFormSource = this.reloadFormSource(
          this._paymentDetailsFormSource,
          {
            amount: suggestedAmount,
          }
        );
      }
    }
  }

  validateForm(): boolean {
    let isAmountValid = true;

    const dataForm = this.formSource.dataForm;
    const amountProperty = dataForm.getPropertyByName('amount');

    // Check and validate amount field
    if (!this._paymentDetailsFormSource.amount) {
      amountProperty.errorMessage = this.translateService.instant(
        'PAYMENT_DETAILS_DIALOG.BODY_SECTION.FORM_CONTROL.AMOUNT_CONTROL.EMPTY_ERROR_MESSAGE'
      );
      isAmountValid = false;
    } else {
      // Check amount if the Amount From Deposit setting is active
      if (
        this._paymentDetailsFormSource.isAmountFromDeposit &&
        this._paymentDetailsFormSource.amount > this._depositedAmountBalance
      ) {
        let errorMessage = this.translateService.instant(
          'PAYMENT_DETAILS_DIALOG.BODY_SECTION.FORM_CONTROL.AMOUNT_FROM_DEPOSIT_CONTROL.EXCEEDED_VALUE_ERROR_MESSAGE'
        );
        errorMessage = errorMessage.replace(
          '{0}',
          NumberFormatter.formatCurrency(this._depositedAmountBalance)
        );

        amountProperty.errorMessage = errorMessage;

        isAmountValid = false;
      } else {
        let totalPaidAmount = 0;
        let totalExpectedAmount = 0;
        let paymentCategoryName = '';

        switch (this._paymentDetailsFormSource.paymentForType) {
          case PaymentForTypeEnum.Rental:
            totalPaidAmount = this._totalPaidRentalAmount;
            totalExpectedAmount = this._totalExpectedRentalAmount;
            paymentCategoryName = this.translateService.instant(
              'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.RENTAL'
            );
            break;
          case PaymentForTypeEnum.ElectricBill:
            totalPaidAmount = this._totalPaidElectricBillAmount;
            totalExpectedAmount = this._totalExpectedElectricBillAmount;
            paymentCategoryName = this.translateService.instant(
              'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.ELECTRIC_BILL'
            );
            break;
          case PaymentForTypeEnum.WaterBill:
            totalPaidAmount = this._totalPaidWaterBillAmount;
            totalExpectedAmount = this._totalExpectedwaterBillAmount;
            paymentCategoryName = this.translateService.instant(
              'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.WATER_BILL'
            );
            break;
          case PaymentForTypeEnum.MiscellaneousFee:
            totalPaidAmount = this._totalPaidMiscellaneousFeesAmount;
            totalExpectedAmount = this._totalExpectedMiscellaneousFeesAmount;
            paymentCategoryName = this.translateService.instant(
              'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.MISCELLANEOUS_FEES'
            );
            break;
          case PaymentForTypeEnum.Penalty:
            totalPaidAmount = this._totalPaidPenaltyAmount;
            totalExpectedAmount = this._totalExpectedPenaltyAmount;
            paymentCategoryName = this.translateService.instant(
              'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.PENALTY'
            );
            break;
          default:
            break;
        }

        // Check if the entered amount + total paid amount already exceeds with the expected total amount.
        if (
          totalPaidAmount + this._paymentDetailsFormSource.amount >
          totalExpectedAmount
        ) {
          let errorMessage = this.translateService.instant(
            'PAYMENT_DETAILS_DIALOG.BODY_SECTION.FORM_CONTROL.AMOUNT_CONTROL.EXCEEDED_VALUE_ERROR_MESSAGE'
          );
          errorMessage = errorMessage.replace('{0}', paymentCategoryName);
          errorMessage = errorMessage.replace('{1}', totalExpectedAmount);
          amountProperty.errorMessage = errorMessage;

          isAmountValid = false;
        } else {
          isAmountValid = true;
        }
      }
    }

    dataForm.notifyValidated('amount', isAmountValid);

    return isAmountValid;
  }

  private setDialogTitle(dialogIntent: DialogIntentEnum) {
    if (dialogIntent === DialogIntentEnum.Add) {
      this._dialogTitle = this.translateService.instant(
        'PAYMENT_DETAILS_DIALOG.HEADER_SECTION.ADD_DIALOG_TITLE'
      );
    } else {
      this._dialogTitle = this.translateService.instant(
        'PAYMENT_DETAILS_DIALOG.HEADER_SECTION.VIEW_DIALOG_TITLE'
      );
    }
  }

  private initFormDetails(paymentDetails: Payment) {
    this._paymentDetailsFormSource =
      this.paymentMapper.mapEntityToFormSource(paymentDetails);
  }

  get paymentMapper(): PaymentMapper {
    return new PaymentMapper();
  }

  get dialogTitle(): string {
    return this._dialogTitle;
  }

  get paymentDetailsFormSource(): PaymentFormSource {
    return this._paymentDetailsFormSource;
  }

  get dialogIntent(): DialogIntentEnum {
    return this._dialogIntent;
  }

  get isForAddNewPayment(): boolean {
    return this._dialogIntent === 1;
  }

  get encashedDepositAmount(): boolean {
    return this._contract?.encashDepositAmount;
  }

  get paymentTypeOptions(): Array<{ key: PaymentForTypeEnum; label: string }> {
    return this._paymentTypeValueProvider.paymentTypeOptions;
  }

  /**
   * @description Identify if "Amount From Deposit" switch should be shown/hidden. Current dialog intent should be for Adding new payment details, Current account statement is not the first, fully paid and there should still be available deposited amount.
   */
  get shouldAmountFromDepositAvailable(): boolean {
    return (
      this._dialogIntent === DialogIntentEnum.View ||
      (this._firstAccountStatement.id !== this._currentAccountStatementId &&
        this._firstAccountStatement.isFullyPaid)
    );
  }
}
