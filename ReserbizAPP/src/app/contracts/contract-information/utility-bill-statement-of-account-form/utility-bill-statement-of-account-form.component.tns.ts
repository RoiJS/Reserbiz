import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { PageRoute, RouterExtensions } from '@nativescript/angular';
import { EventData, Switch } from '@nativescript/core';

import { TranslateService } from '@ngx-translate/core';

import { finalize } from 'rxjs/operators';

import { NumberFormatter } from '@src/app/_helpers/formatters/number-formatter.helper';

import { AccountStatement } from '@src/app/_models/account-statement.model';

import { AccountStatementService } from '@src/app/_services/account-statement.service';
import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';

import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { MiscellaneousDueDateEnum } from '@src/app/_enum/miscellaneous-due-date.enum';
import { AccountStatementTypeEnum } from '@src/app/_enum/account-statement-type.enum';

import { NewAccountStatementDto } from '@src/app/_dtos/new-account-statement.dto';

@Component({
  selector: 'ns-utility-bill-statement-of-account-form',
  templateUrl: './utility-bill-statement-of-account-form.component.html',
  styleUrls: [
    './utility-bill-statement-of-account-form.component.scss',
    '../contract-account-statement-list-panel/contract-account-statement-information/contract-account-statement-information.component.scss',
  ],
})
export class UtilityBillStatementOfAccountFormComponent implements OnInit {
  dueDate: Date;
  private _contractId: number;
  private _isBusy: boolean;
  private _suggestedAccountStatement: AccountStatement;
  private _markAsPaid = false;
  private _waterAndElecitricBillAmountformGroup: FormGroup;

  constructor(
    private accountStatementService: AccountStatementService,
    private contractService: ContractService,
    private dialogService: DialogService,
    private formBuilder: FormBuilder,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe(async (paramMap) => {
        this._contractId = +paramMap.get('contractId');
        this._suggestedAccountStatement =
          await this.accountStatementService.getSuggestedNewAccountStatement(
            this._contractId
          );

        this.dueDate = new Date();
        this._waterAndElecitricBillAmountformGroup.patchValue({
          electricBillAmount: this._suggestedAccountStatement.electricBill,
          waterBillAmount: this._suggestedAccountStatement.waterBill,
        });
      });
    });

    this.initWaterAndElecitricBillAmountForm();
  }

  initWaterAndElecitricBillAmountForm() {
    this._waterAndElecitricBillAmountformGroup = this.formBuilder.group({
      electricBillAmount: [0],
      waterBillAmount: [0],
    });
  }

  saveNewUtilityBillStatementOfAccount() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.NEW_UTILIY_BILL_CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          const waterBillAmount =
            this.waterAndElecitricBillAmountformGroup.get(
              'waterBillAmount'
            ).value;
          const electricBillAmount =
            this.waterAndElecitricBillAmountformGroup.get(
              'electricBillAmount'
            ).value;

          const newStatementOfAccountForUtilityBillsDto =
            new NewAccountStatementDto();

          const updatedWaterBillAmount = waterBillAmount || 0;
          const updatedElectricBillAmount = electricBillAmount || 0;

          newStatementOfAccountForUtilityBillsDto.contractId = this._contractId;
          newStatementOfAccountForUtilityBillsDto.dueDate = this.dueDate;
          newStatementOfAccountForUtilityBillsDto.electricBill =
            updatedElectricBillAmount;
          newStatementOfAccountForUtilityBillsDto.waterBill =
            updatedWaterBillAmount;
          newStatementOfAccountForUtilityBillsDto.accountStatementType =
            AccountStatementTypeEnum.UtilityBilll;

          this.accountStatementService
            .createNewAccountStatement(
              newStatementOfAccountForUtilityBillsDto,
              this._markAsPaid
            )
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.NEW_UTILIY_BILL_SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.accountStatementService.reloadListFlag(true);
                    this.contractService.reloadListFlag();
                    this.router.back();
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.CREATE_NEW_DIALOG.NEW_UTILIY_BILL_ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  onCheckedChange(args: EventData) {
    const sw = args.object as Switch;
    this._markAsPaid = sw.checked;
  }

  get waterAndElecitricBillAmountformGroup(): FormGroup {
    return this._waterAndElecitricBillAmountformGroup;
  }

  get suggestedAccountStatement(): AccountStatement {
    return this._suggestedAccountStatement;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }

  get totalAmount(): string {
    let grandTotal = 0;

    if (this._suggestedAccountStatement) {
      const electricBillAmount =
        this._waterAndElecitricBillAmountformGroup.get(
          'electricBillAmount'
        ).value;
      const waterBillAmount =
        this._waterAndElecitricBillAmountformGroup.get('waterBillAmount').value;

      if (electricBillAmount) {
        grandTotal += parseFloat(electricBillAmount);
      }

      if (waterBillAmount) {
        grandTotal += parseFloat(waterBillAmount);
      }

      if (
        this._suggestedAccountStatement.miscellaneousDueDate ===
        MiscellaneousDueDateEnum.SameWithUtilityBillDueDate
      ) {
        const miscellaneousTotalAmount =
          this._suggestedAccountStatement.miscellaneousTotalAmount;
        grandTotal += miscellaneousTotalAmount;
      }
    }

    return NumberFormatter.formatCurrency(grandTotal);
  }
}
