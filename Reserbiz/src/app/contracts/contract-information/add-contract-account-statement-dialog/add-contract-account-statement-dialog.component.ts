import { Component, OnDestroy, OnInit } from '@angular/core';

import { ModalDialogParams } from '@nativescript/angular';
import { Subscription } from 'rxjs';

import { AccountStatementTypeDropdownControlService } from '../../../_services/account-statement-type-dropdown-control.service';

import { AccountStatementTypeEnum } from '../../../_enum/account-statement-type.enum';

@Component({
  selector: 'ns-add-contract-account-statement-dialog',
  templateUrl: './add-contract-account-statement-dialog.component.html',
  styleUrls: ['./add-contract-account-statement-dialog.component.scss'],
})
export class AddContractAccountStatementDialogComponent
  implements OnInit, OnDestroy
{
  private _contractId = 0;
  private _accountStatementListCount = 0;
  private _accountStatementTypeValueServiceSub: Subscription;

  private _accountStatementType: AccountStatementTypeEnum;

  constructor(
    private accountStatementTypeDropdownValueService: AccountStatementTypeDropdownControlService,
    private params: ModalDialogParams
  ) {
    this._contractId = params.context.contractId;
    this._accountStatementListCount = params.context.accountStatementListCount;
  }

  ngOnInit() {
    this._accountStatementTypeValueServiceSub =
      this.accountStatementTypeDropdownValueService.accountStatementTypeDropdownValue.subscribe(
        (currentValue: AccountStatementTypeEnum) => {
          this._accountStatementType = currentValue;
        }
      );
  }

  ngOnDestroy() {
    if (this._accountStatementTypeValueServiceSub) {
      this._accountStatementTypeValueServiceSub.unsubscribe();
    }
  }

  closeDialog() {
    this.params.closeCallback();
  }

  onCreate() {
    let url = '';

    switch (this._accountStatementType) {
      case AccountStatementTypeEnum.RentalBill:
        url = 'new-account-statement/new-rental-bill-statement-of-account';
        break;
      case AccountStatementTypeEnum.UtilityBilll:
        url = 'new-account-statement/new-utility-bill-statement-of-account';
        break;
    }

    this.params.closeCallback({
      url,
    });
  }

  get contractId(): number {
    return this._contractId;
  }

  get accountStatementListCount(): number {
    return this._accountStatementListCount;
  }
}
