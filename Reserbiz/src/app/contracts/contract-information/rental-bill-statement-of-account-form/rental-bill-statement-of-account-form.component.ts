import { Component, OnInit } from "@angular/core";
import { PageRoute, RouterExtensions } from "@nativescript/angular";
import { finalize } from "rxjs/operators";
import { TranslateService } from "@ngx-translate/core";
import { EventData, Switch } from "@nativescript/core";

import { NewAccountStatementDto } from "~/app/_dtos/new-account-statement.dto";

import { AccountStatement } from "~/app/_models/account-statement.model";

import { AccountStatementTypeEnum } from "~/app/_enum/account-statement-type.enum";
import { MiscellaneousDueDateEnum } from "~/app/_enum/miscellaneous-due-date.enum";

import { NumberFormatter } from "~/app/_helpers/formatters/number-formatter.helper";

import { AccountStatementService } from "~/app/_services/account-statement.service";
import { DialogService } from "~/app/_services/dialog.service";
import { ContractService } from "~/app/_services/contract.service";

@Component({
  selector: "app-rental-bill-statement-of-account-form",
  templateUrl: "./rental-bill-statement-of-account-form.component.html",
  styleUrls: [
    "./rental-bill-statement-of-account-form.component.scss",
    "../contract-account-statement-list-panel/contract-account-statement-information/contract-account-statement-information.component.scss",
  ],
})
export class RentalBillStatementOfAccountFormComponent implements OnInit {
  private _contractId = 0;
  private _isBusy = false;
  private _markAsPaid = false;
  private _suggestedAccountStatement: AccountStatement;

  constructor(
    private accountStatementService: AccountStatementService,
    private contractService: ContractService,
    private dialogService: DialogService,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe(async (paramMap) => {
        this._contractId = +paramMap.get("contractId");
        this._suggestedAccountStatement =
          await this.accountStatementService.getSuggestedNewAccountStatement(
            this._contractId
          );
      });
    });
  }

  saveNewRentalBillStatementOfAccount() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          "ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE"
        ),
        this.translateService.instant(
          "ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.NEW_UTILIY_BILL_CONFIRM_MESSAGE"
        )
      )
      .then((res: boolean) => {
        if (res) {
          this._isBusy = true;

          const newStatementOfAccountForRentalBills =
            new NewAccountStatementDto();

          newStatementOfAccountForRentalBills.contractId = this._contractId;
          newStatementOfAccountForRentalBills.accountStatementType =
            AccountStatementTypeEnum.RentalBill;

          this.accountStatementService
            .createNewAccountStatement(
              newStatementOfAccountForRentalBills,
              this._markAsPaid
            )
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe({
              next: () => {
                this.dialogService
                  .alert(
                    this.translateService.instant(
                      "ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE"
                    ),
                    this.translateService.instant(
                      "ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.NEW_RENTAL_BILL_SUCCESS_MESSAGE"
                    )
                  )
                  .then(() => {
                    this.accountStatementService.reloadListFlag(true);
                    this.contractService.reloadListFlag();
                    this.router.back();
                  });
              },
              error: (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    "ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE"
                  ),
                  this.translateService.instant(
                    "ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.NEW_RENTAL_BILL_ERROR_MESSAGE"
                  )
                );
              },
            });
        }
      });
  }

  onCheckedChange(args: EventData) {
    const sw = args.object as Switch;
    this._markAsPaid = sw.checked;
  }

  get totalAmount(): string {
    let grandTotal = 0;

    if (this._suggestedAccountStatement) {
      const miscellaneousTotalAmount =
        this._suggestedAccountStatement.miscellaneousTotalAmount;
      const rentIncome = this._suggestedAccountStatement.rentIncome;
      const penaltiesTotalAmount =
        this._suggestedAccountStatement.penaltyTotalAmount;

      grandTotal = rentIncome + penaltiesTotalAmount;

      if (
        this._suggestedAccountStatement.miscellaneousDueDate ===
        MiscellaneousDueDateEnum.SameWithRentalDueDate
      ) {
        grandTotal += miscellaneousTotalAmount;
      }
    }

    return NumberFormatter.formatCurrency(grandTotal);
  }

  get suggestedAccountStatement(): AccountStatement {
    return this._suggestedAccountStatement;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}
