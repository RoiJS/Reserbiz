import { SortOrderEnum } from '../_enum/sort-order.enum';

import { IPaymentFilter } from '../_interfaces/ipayment-filter.interface';

import { EntityFilter } from './entity-filter.model';

export class PaymentFilter extends EntityFilter implements IPaymentFilter {
  constructor() {
    super();
    this.sortOrder = SortOrderEnum.Descending;
  }

  reset(): void {
    this.sortOrder = SortOrderEnum.Descending;
  }

  toFilterJSON() {
    return {
      page: this.page,
      accountStatementId: this.parentId,
      sortOrder: this.sortOrder,
    };
  }
}
