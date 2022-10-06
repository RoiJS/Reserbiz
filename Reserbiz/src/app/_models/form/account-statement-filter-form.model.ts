import { BaseForm } from './base-form.model';

import { AccountStatementTypeEnum } from '../../_enum/account-statement-type.enum';
import { SortOrderEnum } from '../../_enum/sort-order.enum';
import { PaymentStatusEnum } from '../../_enum/payment-status.enum';

export class AccountStatementFilterFormSource extends BaseForm<AccountStatementFilterFormSource> {
  constructor(
    public fromDate: Date,
    public toDate: Date,
    public paymentStatus: PaymentStatusEnum,
    public accountStatementType: AccountStatementTypeEnum,
    public sortOrder: SortOrderEnum
  ) {
    super();
  }

  clone() {
    return new AccountStatementFilterFormSource(
      this.fromDate,
      this.toDate,
      this.paymentStatus,
      this.accountStatementType,
      this.sortOrder
    );
  }
}
