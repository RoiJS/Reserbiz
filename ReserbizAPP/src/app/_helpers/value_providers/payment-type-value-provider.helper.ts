import { TranslateService } from '@ngx-translate/core';
import { PaymentForTypeEnum } from '@src/app/_enum/payment-type.enum';

import { IPaymentTypeValueProvider } from '@src/app/_interfaces/value_providers/ipayment-type-value-provider.interface';

export class PaymentTypeValueProvider implements IPaymentTypeValueProvider {
  constructor(
    private translateService: TranslateService,
    private includeAllOption?: boolean
  ) {}

  get paymentTypeOptions(): Array<{ key: PaymentForTypeEnum; label: string }> {
    const options: { key: PaymentForTypeEnum; label: string }[] = [];

    if (this.includeAllOption) {
      options.push({
        key: PaymentForTypeEnum.All,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.ALL'
        ),
      });
    }

    options.push({
      key: PaymentForTypeEnum.Rental,
      label: this.translateService.instant(
        'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.RENTAL'
      ),
    });

    options.push({
      key: PaymentForTypeEnum.ElectricBill,
      label: this.translateService.instant(
        'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.ELECTRIC_BILL'
      ),
    });

    options.push({
      key: PaymentForTypeEnum.WaterBill,
      label: this.translateService.instant(
        'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.WATER_BILL'
      ),
    });

    options.push({
      key: PaymentForTypeEnum.MiscellaneousFee,
      label: this.translateService.instant(
        'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.MISCELLANEOUS_FEES'
      ),
    });

    options.push({
      key: PaymentForTypeEnum.Penalty,
      label: this.translateService.instant(
        'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.PENALTY'
      ),
    });

    return options;
  }
}
