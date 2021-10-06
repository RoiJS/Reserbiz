import { AccountStatementTypeEnum } from '@src/app/_enum/account-statement-type.enum';
import { BaseForm } from './base-form.model';

export class AccountStatementTypeDropdownControlFormSource extends BaseForm<AccountStatementTypeDropdownControlFormSource> {
  constructor(public accountStatementType: AccountStatementTypeEnum) {
    super();
  }

  clone() {
    return new AccountStatementTypeDropdownControlFormSource(
      this.accountStatementType
    );
  }
}
