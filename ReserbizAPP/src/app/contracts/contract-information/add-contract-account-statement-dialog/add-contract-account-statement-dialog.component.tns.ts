import { Component, OnInit } from '@angular/core';

import { EventData, Switch } from '@nativescript/core';

import { ModalDialogParams } from '@nativescript/angular';

import { AccountStatement } from '@src/app/_models/account-statement.model';

@Component({
  selector: 'ns-add-contract-account-statement-dialog',
  templateUrl: './add-contract-account-statement-dialog.component.html',
  styleUrls: ['./add-contract-account-statement-dialog.component.scss'],
})
export class AddContractAccountStatementDialogComponent implements OnInit {
  private _suggestedAccountStatement: AccountStatement;

  private _markAsPaid: boolean;

  constructor(private params: ModalDialogParams) {
    this._suggestedAccountStatement = params.context.suggestedAccountStatement;
  }

  ngOnInit() {}

  closeDialog() {
    this.params.closeCallback();
  }

  onCreate() {
    this.params.closeCallback({
      confirm: true,
      markAsPaid: this._markAsPaid,
    });
  }

  onCheckedChange(args: EventData) {
    const sw = args.object as Switch;
    this._markAsPaid = sw.checked;
  }

  get suggestedAccountStatement(): AccountStatement {
    return this._suggestedAccountStatement;
  }
}
