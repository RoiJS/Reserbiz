import { PaymentForTypeEnum } from '@src/app/_enum/payment-type.enum';

export interface IPaymentTypeValueProvider {
  paymentTypeOptions: Array<{ key: PaymentForTypeEnum; label: string }>;
}
