import { TranslateService } from '@ngx-translate/core';
import { PaymentForTypeEnum } from '@src/app/_enum/payment-type.enum';

import { IPaymentTypeValueProvider } from '@src/app/_interfaces/value_providers/ipayment-type-value-provider.interface';

export class PaymentTypeValueProvider implements IPaymentTypeValueProvider {
  constructor(private translateService: TranslateService) {}

  get paymentTypeOptions(): Array<{ key: PaymentForTypeEnum; label: string }> {
    return [
      {
        key: PaymentForTypeEnum.Rental,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.RENTAL'
        ),
      },
      {
        key: PaymentForTypeEnum.ElectricBill,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.ELECTRIC_BILL'
        ),
      },
      {
        key: PaymentForTypeEnum.WaterBill,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.WATER_BILL'
        ),
      },
      {
        key: PaymentForTypeEnum.MiscellaneousFee,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.MISCELLANEOUS_FEES'
        ),
      },
      {
        key: PaymentForTypeEnum.Penalty,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.PENALTY'
        ),
      },
    ];
  }
}
