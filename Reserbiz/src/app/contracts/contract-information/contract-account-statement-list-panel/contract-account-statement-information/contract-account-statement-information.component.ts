import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

import { PageRoute, RouterExtensions } from '@nativescript/angular';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { ExtendedNavigationExtras } from '@nativescript/angular/lib/legacy/router/router-extensions';
import { TranslateService } from '@ngx-translate/core';

import { AccountStatement } from '../../../../_models/account-statement.model';
import { Contract } from '../../../../_models/contract.model';

import { ButtonOptions } from '../../../../_enum/button-options.enum';

import { AccountStatementService } from '../../../../_services/account-statement.service';
import { ContractService } from '../../../../_services/contract.service';
import { DialogService } from '../../../../_services/dialog.service';
import { UserNotificationService } from '../../../../_services/user-notification.service';

import { NumberFormatter } from '../../../../_helpers/formatters/number-formatter.helper';

@Component({
  selector: 'app-contract-account-statement-information',
  templateUrl: './contract-account-statement-information.component.html',
  styleUrls: ['./contract-account-statement-information.component.scss'],
})
export class ContractAccountStatementInformationComponent
  implements OnInit, OnDestroy
{
  private _currentAccountStatement: AccountStatement;
  private _currentAccountStatementId: number;
  private _isBusy = false;

  private _waterAndElecitricBillAmountformGroup: FormGroup;
  private _updateAccountStatementListFlag: Subscription;
  private _lastDateSent: string;

  dueDate: Date;
  private dueDateOriginal: Date;
  private waterBillAmountOriginal: number;
  private electricBillAmountOriginal: number;
  private _contract: Contract;

  constructor(
    private activatedRoute: ActivatedRoute,
    private accountStatementService: AccountStatementService,
    private contractService: ContractService,
    private dialogService: DialogService,
    private userNotificationService: UserNotificationService,
    private formBuilder: FormBuilder,
    private pageRoute: PageRoute,
    private translateService: TranslateService,
    private router: RouterExtensions
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentAccountStatementId = +paramMap.get('accountStatementId');
        const contractId = +paramMap.get('contractId');

        this._updateAccountStatementListFlag =
          this.accountStatementService.loadAccountStatementListFlag.subscribe(
            () => {
              this.getAccountStatementInformation(contractId);
            }
          );
      });
    });

    this.initWaterAndElecitricBillAmountForm();
  }

  ngOnDestroy() {
    this._updateAccountStatementListFlag.unsubscribe();
  }

  getAccountStatementInformation(contractId: number) {
    this._isBusy = true;

    (async () => {
      this._contract = await this.contractService.getContract(contractId);

      this._currentAccountStatement =
        await this.accountStatementService.getAccountStatement(
          this._currentAccountStatementId
        );

      this._lastDateSent = this._currentAccountStatement.lastDateSentFormatted;

      this.dueDate = this._currentAccountStatement.dueDate;
      this._waterAndElecitricBillAmountformGroup.patchValue({
        electricBillAmount: this._currentAccountStatement.electricBill,
        waterBillAmount: this._currentAccountStatement.waterBill,
      });

      this.electricBillAmountOriginal =
        this._currentAccountStatement.electricBill;
      this.waterBillAmountOriginal = this._currentAccountStatement.waterBill;
      this.dueDateOriginal = this._currentAccountStatement.dueDate;

      this._isBusy = false;

      // Make sure to reload unread notification count.
      this.userNotificationService.checkNotificationUpdate();
    })();
  }

  initWaterAndElecitricBillAmountForm() {
    this._waterAndElecitricBillAmountformGroup = this.formBuilder.group({
      electricBillAmount: [0],
      waterBillAmount: [0],
    });
  }

  updateInformation() {
    const waterBillAmount =
      this.waterAndElecitricBillAmountformGroup.get('waterBillAmount').value;
    const electricBillAmount =
      this.waterAndElecitricBillAmountformGroup.get('electricBillAmount').value;

    if (
      this.checkedInputChanged(
        electricBillAmount,
        waterBillAmount,
        this.dueDate
      )
    ) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'ACCOUNT_STATEMENT_DETAILS.UPDATE_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .subscribe((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            (async () => {
              try {
                const updatedWaterBillAmount = waterBillAmount || 0;
                const updatedElectricBillAmount = electricBillAmount || 0;

                await this.accountStatementService.updateWaterAndElectricBillAmount(
                  this._currentAccountStatement.id,
                  updatedWaterBillAmount,
                  updatedElectricBillAmount,
                  this.dueDate
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

  deleteSelectedItem() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'ACCOUNT_STATEMENT_DETAILS.DELETE_ACCOUNT_STATEMENT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'ACCOUNT_STATEMENT_DETAILS.DELETE_ACCOUNT_STATEMENT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .subscribe((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.accountStatementService
            .deleteItem(this._currentAccountStatementId)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.DELETE_ACCOUNT_STATEMENT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.DELETE_ACCOUNT_STATEMENT_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.contractService.reloadListFlag();
                    this.accountStatementService.reloadListFlag(true);
                    this.router.back();
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.DELETE_ACCOUNT_STATEMENT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'ACCOUNT_STATEMENT_DETAILS.DELETE_ACCOUNT_STATEMENT_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  checkedInputChanged(
    electricBillAmount: number,
    waterBillAmount: number,
    dueDate: Date
  ): boolean {
    const result =
      electricBillAmount !== this.electricBillAmountOriginal ||
      waterBillAmount !== this.waterBillAmountOriginal ||
      dueDate !== this.dueDateOriginal;
    return result;
  }

  sendAccountStatementDetails() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'ACCOUNT_STATEMENT_DETAILS.SEND_DETAILS_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'ACCOUNT_STATEMENT_DETAILS.SEND_DETAILS_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .subscribe((result: ButtonOptions) => {
        if (result === ButtonOptions.YES) {
          this._isBusy = true;
          (async () => {
            try {
              await this.accountStatementService.sendAccountStatementDetails(
                this._currentAccountStatementId
              );

              this.accountStatementService.reloadListFlag(true);

              this.dialogService.alert(
                this.translateService.instant(
                  'ACCOUNT_STATEMENT_DETAILS.SEND_DETAILS_DIALOG.TITLE'
                ),
                this.translateService.instant(
                  'ACCOUNT_STATEMENT_DETAILS.SEND_DETAILS_DIALOG.SUCCESS_MESSAGE'
                )
              );

              this._isBusy = false;
            } catch {
              this.dialogService.alert(
                this.translateService.instant(
                  'ACCOUNT_STATEMENT_DETAILS.SEND_DETAILS_DIALOG.TITLE'
                ),
                this.translateService.instant(
                  'ACCOUNT_STATEMENT_DETAILS.SEND_DETAILS_DIALOG.ERROR_MESSAGE'
                )
              );
            }
          })();
        }
      });
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
      const electricBillAmount =
        this._waterAndElecitricBillAmountformGroup.get(
          'electricBillAmount'
        ).value;
      const waterBillAmount =
        this._waterAndElecitricBillAmountformGroup.get('waterBillAmount').value;

      const miscellaneousTotalAmount =
        this._currentAccountStatement.miscellaneousTotalAmount;
      const rentIncome = this._currentAccountStatement.rentIncome;
      const penaltiesTotalAmount =
        this._currentAccountStatement.penaltyTotalAmount;

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

  get pageTitle(): string {
    return this._contract?.code;
  }

  get sendDetailsButtonText(): string {
    return `${String.fromCharCode(0xf0e0)} ${this.translateService.instant(
      'ACCOUNT_STATEMENT_DETAILS.SEND_DETAILS'
    )}`;
  }

  get lastDateSentFormatted(): string {
    return this._lastDateSent;
  }
}
