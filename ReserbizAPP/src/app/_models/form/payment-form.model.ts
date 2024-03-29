import { PaymentForTypeEnum } from '@src/app/_enum/payment-type.enum';
import { IBaseFormSource } from '../../_interfaces/ibase-form-source.interface';
import { BaseForm } from './base-form.model';

export class PaymentFormSource
  extends BaseForm<PaymentFormSource>
  implements IBaseFormSource<PaymentFormSource>
{
  constructor(
    public dateReceived: Date,
    public timeReceived: Date,
    public amount: number,
    public notes: string,
    public isAmountFromDeposit: boolean,
    public paymentForType: PaymentForTypeEnum,
    public receivedBy: string
  ) {
    super();
  }

  clone() {
    return new PaymentFormSource(
      this.dateReceived,
      this.timeReceived,
      this.amount,
      this.notes,
      this.isAmountFromDeposit,
      this.paymentForType,
      this.receivedBy
    );
  }
}
