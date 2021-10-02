import { AccountStatementTypeEnum } from '@src/app/_enum/account-statement-type.enum';
import { PaymentStatusEnum } from '../../_enum/payment-status.enum';
import { SortOrderEnum } from '../../_enum/sort-order.enum';

import { DateFormatter } from '../../_helpers/formatters/date-formatter.helper';

import { IAccountStatementFilter } from '../../_interfaces/filters/iaccount-statement-filter.interface';

import { EntityFilter } from './entity-filter.model';

export class AccountStatementFilter
  extends EntityFilter
  implements IAccountStatementFilter
{
  public fromDate: Date;
  public toDate: Date;
  public paymentStatus: PaymentStatusEnum;
  public accountStatementType: AccountStatementTypeEnum;

  constructor() {
    super();
    this.fromDate = null;
    this.toDate = null;
    this.paymentStatus = PaymentStatusEnum.All;
    this.accountStatementType = AccountStatementTypeEnum.All;
    this.sortOrder = SortOrderEnum.Descending;
  }

  reset(): void {
    this.fromDate = null;
    this.toDate = null;
    this.paymentStatus = PaymentStatusEnum.All;
    this.accountStatementType = AccountStatementTypeEnum.All;
    this.sortOrder = SortOrderEnum.Descending;
  }

  isFilterActive(): boolean {
    const hasFilter = Boolean(
      this.fromDate ||
        this.toDate ||
        this.paymentStatus !== PaymentStatusEnum.All ||
        this.accountStatementType !== AccountStatementTypeEnum.All ||
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
      accountStatementType: this.accountStatementType,
      sortOrder: this.sortOrder,
    };
  }
}
