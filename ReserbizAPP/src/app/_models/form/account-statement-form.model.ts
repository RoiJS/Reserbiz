import { BaseForm } from './base-form.model';

export class AccountStatementFormSource extends BaseForm<
  AccountStatementFormSource
> {
  constructor(public electricBill: number, public waterBill: number) {
    super();
  }

  clone(): AccountStatementFormSource {
    return new AccountStatementFormSource(this.electricBill, this.waterBill);
  }
}
