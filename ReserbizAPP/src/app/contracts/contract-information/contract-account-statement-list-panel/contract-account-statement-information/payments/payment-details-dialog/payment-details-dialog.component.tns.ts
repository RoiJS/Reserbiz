import { Component, OnInit, ViewChild } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { ModalDialogParams } from 'nativescript-angular';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';
import { DataFormEventData } from 'nativescript-ui-dataform';

import { AccountStatement } from '@src/app/_models/account-statement.model';
import { BaseFormHelper } from '@src/app/_helpers/base-form.helper';
import { NumberFormatter } from '@src/app/_helpers/number-formatter.helper';
import { PaymentMapper } from '@src/app/_helpers/payment-mapper.helper';

import { DialogService } from '@src/app/_services/dialog.service';

import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { DialogIntentEnum } from '@src/app/_enum/dialog-intent.enum';

import { Payment } from '@src/app/_models/payment.model';
import { PaymentFormSource } from '@src/app/_models/payment-form.model';


@Component({
  selector: 'ns-payment-details-dialog',
  templateUrl: './payment-details-dialog.component.html',
  styleUrls: ['./payment-details-dialog.component.scss'],
})
export class PaymentDetailsDialogComponent
  extends BaseFormHelper<PaymentFormSource>
  implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  formSource: RadDataFormComponent;

  private _dialogTitle = '';
  private _dialogIntent: DialogIntentEnum;

  private _suggestedAmountForPayment = 0;
  private _depositedAmountBalance = 0;
  private _currentAccountStatementId = 0;

  private _firstAccountStatement = new AccountStatement();

  private _paymentDetailsFormSource: PaymentFormSource;

  constructor(
    private dialogService: DialogService,
    private params: ModalDialogParams,
    private translateService: TranslateService
  ) {
    super();
    this.setDialogTitle(params.context.dialogIntent);
    this.initFormDetails(params.context.paymentDetails);

    this._dialogIntent = params.context.dialogIntent;
    this._currentAccountStatementId = params.context.currentAccountStatementId;
    this._firstAccountStatement = params.context.firstAccountStatement;
    this._suggestedAmountForPayment = params.context.suggestedAmountForPayment;
    this._depositedAmountBalance = params.context.depositedAmountBalance;
  }

  ngOnInit() {}

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
    if (args.propertyName === 'isAmountFromDeposit') {
      if (
        this._paymentDetailsFormSource.isAmountFromDeposit &&
        this._paymentDetailsFormSource.amount === 0
      ) {
        this._paymentDetailsFormSource = this.reloadFormSource(
          this._paymentDetailsFormSource,
          {
            amount: this._suggestedAmountForPayment,
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
        isAmountValid = true;
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
    this._paymentDetailsFormSource = this.paymentMapper.mapEntityToFormSource(
      paymentDetails
    );
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
