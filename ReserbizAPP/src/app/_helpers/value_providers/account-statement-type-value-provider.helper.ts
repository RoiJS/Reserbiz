import { TranslateService } from '@ngx-translate/core';
import { AccountStatementTypeEnum } from '@src/app/_enum/account-statement-type.enum';
import { IAccountStatementTypeValueProvider } from '@src/app/_interfaces/value_providers/iaccount-statement-type-value-provider.interface';

export class AccountStatementTypeValueProvider
  implements IAccountStatementTypeValueProvider
{
  constructor(
    private translateService: TranslateService,
    private includeUtilityBills: boolean
  ) {}
  get accountStatementTypeOptions(): Array<{
    key: AccountStatementTypeEnum;
    label: string;
  }> {
    const options: Array<{
      key: AccountStatementTypeEnum;
      label: string;
    }> = [];

    options.push({
      key: AccountStatementTypeEnum.RentalBill,
      label: this.translateService.instant(
        'GENERAL_TEXTS.ACCOUNT_STATEMENT_TYPE.RENTAL_BILL'
      ),
    });

    // Only include the Utility Bills option
    // if both electric and water bills are not set
    // to be included on the contract rate and
    // also if the account statement is not the first.
    if (!this.includeUtilityBills) {
      options.push({
        key: AccountStatementTypeEnum.UtilityBilll,
        label: this.translateService.instant(
          'GENERAL_TEXTS.ACCOUNT_STATEMENT_TYPE.UTILITY_BILL'
        ),
      });
    }

    return options;
  }
}
