import { Component, OnInit, ViewChild } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { ModalDialogParams } from 'nativescript-angular';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';
import { DataFormEventData } from 'nativescript-ui-dataform';

import { PaymentMapper } from '@src/app/_helpers/payment-mapper.helper';
import { BaseFormHelper } from '@src/app/_helpers/base-form.helper';

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
  }

  ngOnInit() {}

  onSave() {
    const isFormInvalid = this.formSource.dataForm.hasValidationErrors();

    if (!isFormInvalid) {
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
}
