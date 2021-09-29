import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { DataFormEventData } from 'nativescript-ui-dataform';

import { AccountStatementTypeEnum } from '@src/app/_enum/account-statement-type.enum';

import { BaseFormHelper } from '@src/app/_helpers/base_helpers/base-form.helper';
import { AccountStatementTypeValueProvider } from '@src/app/_helpers/value_providers/account-statement-type-value-provider.helper';

import { AccountStatementTypeDropdownControlFormSource } from '@src/app/_models/form/account-statement-type-dropdown-control-form.model';

import { AccountStatementTypeDropdownControlService } from '@src/app/_services/account-statement-type-dropdown-control.service';
import { AccountStatementService } from '@src/app/_services/account-statement.service';

@Component({
  selector: 'ns-account-statement-type-dropdown-control',
  templateUrl: './account-statement-type-dropdown-control.component.html',
  styleUrls: ['./account-statement-type-dropdown-control.component.scss'],
})
export class AccountStatementTypeDropdownControlComponent
  extends BaseFormHelper<AccountStatementTypeDropdownControlFormSource>
  implements OnInit, OnChanges
{
  @Input() contractId: number;
  @Input() accountStatementListCount: number;

  private _accountStatementTypeDropdownControlFormSource: AccountStatementTypeDropdownControlFormSource;
  private _accountStatementTypeValueProvider: AccountStatementTypeValueProvider;

  constructor(
    private accountStatementService: AccountStatementService,
    private accountStatementTypeDropdownValueService: AccountStatementTypeDropdownControlService,
    private translateService: TranslateService
  ) {
    super();

    // Make sure to set the Rental Bill as default.
    this.accountStatementTypeDropdownValueService.accountStatementTypeDropdownValue.next(
      AccountStatementTypeEnum.RentalBill
    );

    this._accountStatementTypeDropdownControlFormSource =
      new AccountStatementTypeDropdownControlFormSource(
        AccountStatementTypeEnum.RentalBill
      );
  }

  ngOnInit() {
    this._accountStatementTypeValueProvider =
      new AccountStatementTypeValueProvider(this.translateService, true);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.contractId.currentValue) {
      const contractId = changes.contractId.currentValue;
      const accountStatementListCount =
        changes.accountStatementListCount.currentValue;

      (async () => {
        const suggestedAccountStatement =
          await this.accountStatementService.getSuggestedNewAccountStatement(
            contractId
          );

        // Only include the Utility Bills option
        // if both electric and water bills are not set
        // to be included on the contract rate and
        // also if the account statement is not the first.
        const includeUtilityBills = Boolean(
          (!suggestedAccountStatement.excludeElectricBill &&
            !suggestedAccountStatement.excludeWaterBill) ||
            accountStatementListCount === 0
        );

        this._accountStatementTypeValueProvider =
          new AccountStatementTypeValueProvider(
            this.translateService,
            includeUtilityBills
          );
      })();
    }
  }

  onPropertyCommitted(args: DataFormEventData) {
    this.accountStatementTypeDropdownValueService.accountStatementTypeDropdownValue.next(
      this._accountStatementTypeDropdownControlFormSource.accountStatementType
    );
  }

  get accountStatementTypeDropdownControlFormSource(): AccountStatementTypeDropdownControlFormSource {
    return this._accountStatementTypeDropdownControlFormSource;
  }

  get accountStatementTypeOptions(): Array<{
    key: AccountStatementTypeEnum;
    label: string;
  }> {
    return this._accountStatementTypeValueProvider.accountStatementTypeOptions;
  }
}
