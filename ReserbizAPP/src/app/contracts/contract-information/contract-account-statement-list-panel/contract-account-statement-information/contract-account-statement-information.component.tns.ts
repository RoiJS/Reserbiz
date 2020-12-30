import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

import { PageRoute, RouterExtensions } from 'nativescript-angular';
import { ExtendedNavigationExtras } from 'nativescript-angular/router/router-extensions';
import { Subscription } from 'rxjs';

import { TranslateService } from '@ngx-translate/core';

import { AccountStatement } from '@src/app/_models/account-statement.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

import { AccountStatementService } from '@src/app/_services/account-statement.service';
import { DialogService } from '@src/app/_services/dialog.service';

import { NumberFormatter } from '@src/app/_helpers/number-formatter.helper';

@Component({
  selector: 'app-contract-account-statement-information',
  templateUrl: './contract-account-statement-information.component.html',
  styleUrls: ['./contract-account-statement-information.component.scss'],
})
export class ContractAccountStatementInformationComponent
  implements OnInit, OnDestroy {
  private _currentAccountStatement: AccountStatement;
  private _currentAccountStatementId: number;
  private _isBusy = false;

  private _waterAndElecitricBillAmountformGroup: FormGroup;
  private _updateAccountStatementListFlag: Subscription;

  private waterBillAmountOriginal: number;
  private electricBillAmountOriginal: number;

  constructor(
    private activatedRoute: ActivatedRoute,
    private accountStatementService: AccountStatementService,
    private dialogService: DialogService,
    private formBuilder: FormBuilder,
    private pageRoute: PageRoute,
    private translateService: TranslateService,
    private router: RouterExtensions
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentAccountStatementId = +paramMap.get('accountStatmentId');

        this._updateAccountStatementListFlag = this.accountStatementService.loadAccountStatementListFlag.subscribe(
          () => {
            this.getAccountStatmentInformation();
          }
        );
      });
    });

    this.initWaterAndElecitricBillAmountForm();
  }

  ngOnDestroy() {
    this._updateAccountStatementListFlag.unsubscribe();
  }

  getAccountStatmentInformation() {
    this._isBusy = true;

    (async () => {
      this._currentAccountStatement = await this.accountStatementService.getAccountStatement(
        this._currentAccountStatementId
      );

      this._waterAndElecitricBillAmountformGroup.patchValue({
        electricBillAmount: this._currentAccountStatement.electricBill,
        waterBillAmount: this._currentAccountStatement.waterBill,
      });

      this.electricBillAmountOriginal = this._currentAccountStatement.electricBill;
      this.waterBillAmountOriginal = this._currentAccountStatement.waterBill;

      this._isBusy = false;
    })();
  }

  initWaterAndElecitricBillAmountForm() {
    this._waterAndElecitricBillAmountformGroup = this.formBuilder.group({
      electricBillAmount: [0],
      waterBillAmount: [0],
    });
  }

  updateInformation() {
    const waterBillAmount = this.waterAndElecitricBillAmountformGroup.get(
      'waterBillAmount'
    ).value;
    const electricBillAmount = this.waterAndElecitricBillAmountformGroup.get(
      'electricBillAmount'
    ).value;

    if (this.checkedInputChanged(electricBillAmount, waterBillAmount)) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            (async () => {
              try {
                await this.accountStatementService.updateWaterAndElectricBillAmount(
                  this._currentAccountStatement.id,
                  waterBillAmount,
                  electricBillAmount
                );

                this.dialogService.alert(
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.accountStatementService.reloadListFlag(true);
                    this.router.back();
                  }
                );

                this.electricBillAmountOriginal = electricBillAmount;
                this.waterBillAmountOriginal = waterBillAmount;
              } catch {
                this.dialogService.alert(
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.ERROR_MESSAGE'
                  )
                );
              } finally {
                this._isBusy = false;
              }
            })();
          }
        });
    }
  }

  checkedInputChanged(
    electricBillAmount: number,
    waterBillAmount: number
  ): boolean {
    const result =
      electricBillAmount !== this.electricBillAmountOriginal ||
      waterBillAmount !== this.waterBillAmountOriginal;
    return result;
  }

  get currentAccountStatement(): AccountStatement {
    return this._currentAccountStatement;
  }

  get waterAndElecitricBillAmountformGroup(): FormGroup {
    return this._waterAndElecitricBillAmountformGroup;
  }
  get IsBusy(): boolean {
    return this._isBusy;
  }

  get totalAmount(): string {
    let grandTotal = 0;

    if (this._currentAccountStatement) {
      const electricBillAmount = this._waterAndElecitricBillAmountformGroup.get(
        'electricBillAmount'
      ).value;
      const waterBillAmount = this._waterAndElecitricBillAmountformGroup.get(
        'waterBillAmount'
      ).value;

      const miscellaneousTotalAmount = this._currentAccountStatement
        .miscellaneousTotalAmount;
      const rentIncome = this._currentAccountStatement.rentIncome;
      const penaltiesTotalAmount = this._currentAccountStatement
        .penaltyTotalAmount;

      grandTotal = miscellaneousTotalAmount + rentIncome + penaltiesTotalAmount;

      if (electricBillAmount) {
        grandTotal += parseFloat(electricBillAmount);
      }
      if (waterBillAmount) {
        grandTotal += parseFloat(waterBillAmount);
      }
    }

    return NumberFormatter.formatCurrency(grandTotal);
  }

  navigateToOtherPage(mainUrl: string) {
    setTimeout(() => {
      mainUrl = mainUrl.replace(
        ':id',
        this._currentAccountStatementId.toString()
      );

      const routeConfig: ExtendedNavigationExtras = {
        transition: {
          name: 'slideLeft',
        },
      };

      routeConfig.relativeTo = this.activatedRoute;

      this.router.navigate([mainUrl], routeConfig);
    }, 100);
  }
}
