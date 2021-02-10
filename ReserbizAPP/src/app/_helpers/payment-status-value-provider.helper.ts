import { TranslateService } from '@ngx-translate/core';
import { PaymentStatusEnum } from '../_enum/payment-status.enum';

import { IPaymentStatusValueProvider } from '../_interfaces/ipayment-status-value-provider.interface.tns';

export class PaymentStatusValueProvider implements IPaymentStatusValueProvider {
  constructor(private translateService: TranslateService) {}

  get paymentStatusOptions(): Array<{ key: PaymentStatusEnum; label: string }> {
    return [
      {
        key: PaymentStatusEnum.All,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_STATUS.ALL'
        ),
      },
      {
        key: PaymentStatusEnum.Paid,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_STATUS.PAID'
        ),
      },
      {
        key: PaymentStatusEnum.Unpaid,
        label: this.translateService.instant(
          'GENERAL_TEXTS.PAYMENT_STATUS.UNPAID'
        ),
      },
    ];
  }
}
