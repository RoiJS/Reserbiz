import { AccountStatementTypeEnum } from '@src/app/_enum/account-statement-type.enum';

export interface IAccountStatementTypeValueProvider {
  accountStatementTypeOptions: Array<{
    key: AccountStatementTypeEnum;
    label: string;
  }>;
}
