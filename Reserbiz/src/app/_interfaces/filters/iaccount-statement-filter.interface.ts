import { AccountStatementTypeEnum } from '../../_enum/account-statement-type.enum';
import { PaymentStatusEnum } from '../../_enum/payment-status.enum';
import { IEntityFilter } from './ientity-filter.interface';

export interface IAccountStatementFilter extends IEntityFilter {
  fromDate: Date;
  toDate: Date;
  paymentStatus: PaymentStatusEnum;
  accountStatementType: AccountStatementTypeEnum;
}
