import { PaymentForTypeEnum } from '../../_enum/payment-type.enum';
import { SortOrderEnum } from '../../_enum/sort-order.enum';

import { IPaymentFilter } from '../../_interfaces/filters/ipayment-filter.interface';

import { EntityFilter } from './entity-filter.model';

export class PaymentFilter extends EntityFilter implements IPaymentFilter {
  public contractId: number;
  public paymentForType: PaymentForTypeEnum;

  constructor() {
    super();
    this.sortOrder = SortOrderEnum.Descending;
    this.paymentForType = PaymentForTypeEnum.All;
  }

  reset(): void {
    this.sortOrder = SortOrderEnum.Descending;
    this.paymentForType = PaymentForTypeEnum.All;
  }

  isFilterActive(): boolean {
    const hasFilter = Boolean(
      this.paymentForType !== PaymentForTypeEnum.All ||
        this.sortOrder === SortOrderEnum.Ascending
    );

    return hasFilter;
  }
  toFilterJSON() {
    return {
      page: this.page,
      accountStatementId: this.parentId,
      contractId: this.contractId,
      sortOrder: this.sortOrder,
      paymentForType: this.paymentForType,
    };
  }
}
