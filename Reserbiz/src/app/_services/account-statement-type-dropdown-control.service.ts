import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { AccountStatementTypeEnum } from '../_enum/account-statement-type.enum';

@Injectable({
  providedIn: 'root',
})
export class AccountStatementTypeDropdownControlService {
  private _accountStatementTypeDropdownValue =
    new BehaviorSubject<AccountStatementTypeEnum>(
      AccountStatementTypeEnum.RentalBill
    );
  constructor() {}

  get accountStatementTypeDropdownValue(): BehaviorSubject<AccountStatementTypeEnum> {
    return this._accountStatementTypeDropdownValue;
  }
}
