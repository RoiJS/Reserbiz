import { IBaseFormSource } from '../_interfaces/ibase-form-source.interface';
import { BaseForm } from './base-form.model';

export class PaymentFormSource
  extends BaseForm<PaymentFormSource>
  implements IBaseFormSource<PaymentFormSource> {
  constructor(
    public dateTimeReceived: Date,
    public amount: number,
    public notes: string,
    public receivedBy: string
  ) {
    super();
  }

  clone() {
    return new PaymentFormSource(
      this.dateTimeReceived,
      this.amount,
      this.notes,
      this.receivedBy
    );
  }
}
