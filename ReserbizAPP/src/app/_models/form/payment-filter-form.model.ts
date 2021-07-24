import { BaseForm } from './base-form.model';

import { SortOrderEnum } from '../../_enum/sort-order.enum';
import { PaymentForTypeEnum } from '@src/app/_enum/payment-type.enum';

export class PaymentFilterFormSource extends BaseForm<PaymentFilterFormSource> {
  constructor(
    public paymentForType: PaymentForTypeEnum,
    public sortOrder: SortOrderEnum
  ) {
    super();
  }

  clone() {
    return new PaymentFilterFormSource(this.paymentForType, this.sortOrder);
  }
}
