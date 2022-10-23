import { TranslateService } from '@ngx-translate/core';
import { PaymentForTypeEnum } from '../../_enum/payment-type.enum';

import { IPaymentTypeValueProvider } from '../../_interfaces/value_providers/ipayment-type-value-provider.interface';

export class PaymentTypeValueProvider implements IPaymentTypeValueProvider {
  constructor(
    private translateService: TranslateService,
    private includeAllOption: boolean,
    private showRentalBillsOptions: boolean,
    private showUtilityBillsOptions: boolean,
    private showMiscellaneousFeesOption: boolean
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

    if (this.showRentalBillsOptions) {
      options.push({
        key: PaymentForTypeEnum.Rental,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.RENTAL'
        ),
      });

      options.push({
        key: PaymentForTypeEnum.Penalty,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.PENALTY'
        ),
      });
    }

    if (this.showUtilityBillsOptions) {
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
    }

    if (this.showMiscellaneousFeesOption) {
      options.push({
        key: PaymentForTypeEnum.MiscellaneousFee,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_TYPE_OPTIONS.MISCELLANEOUS_FEES'
        ),
      });
    }

    return options;
  }
}
