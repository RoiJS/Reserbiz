import { PaymentStatusEnum } from '../_enum/payment-status.enum';
import { SortOrderEnum } from '../_enum/sort-order.enum';

import { DateFormatter } from '../_helpers/date-formatter.helper';

import { IAccountStatementFilter } from '../_interfaces/iaccount-statement-filter.interface';

import { EntityFilter } from './entity-filter.model';

export class AccountStatementFilter
  extends EntityFilter
  implements IAccountStatementFilter {
  public fromDate: Date;
  public toDate: Date;
  public paymentStatus: PaymentStatusEnum;

  constructor() {
    super();
    this.fromDate = null;
    this.toDate = null;
    this.paymentStatus = PaymentStatusEnum.All;
    this.sortOrder = SortOrderEnum.Descending;
  }

  reset(): void {
    this.fromDate = null;
    this.toDate = null;
    this.paymentStatus = PaymentStatusEnum.All;
    this.sortOrder = SortOrderEnum.Descending;
  }

  isFilterActive(): boolean {
    const hasFilter = Boolean(
      this.fromDate ||
        this.toDate ||
        this.paymentStatus !== PaymentStatusEnum.All ||
        this.sortOrder !== SortOrderEnum.Descending
    );

    return hasFilter;
  }

  toFilterJSON() {
    return {
      page: this.page,
      contractId: this.parentId,
      fromDate: DateFormatter.format(this.fromDate),
      toDate: DateFormatter.format(this.toDate),
      paymentStatus: this.paymentStatus,
      sortOrder: this.sortOrder,
    };
  }
}
