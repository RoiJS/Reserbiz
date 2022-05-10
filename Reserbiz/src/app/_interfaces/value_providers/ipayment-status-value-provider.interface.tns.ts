import { PaymentStatusEnum } from '../../_enum/payment-status.enum';

export interface IPaymentStatusValueProvider {
  paymentStatusOptions: Array<{ key: PaymentStatusEnum; label: string }>;
}
