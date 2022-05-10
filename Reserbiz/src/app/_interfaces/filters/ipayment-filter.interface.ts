import { IEntityFilter } from './ientity-filter.interface';
import { PaymentForTypeEnum } from '../../_enum/payment-type.enum';

export interface IPaymentFilter extends IEntityFilter {
  contractId: number;
  paymentForType: PaymentForTypeEnum;
}
