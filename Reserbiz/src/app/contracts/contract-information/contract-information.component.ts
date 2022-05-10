import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';

import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { PageRoute, RouterExtensions } from '@nativescript/angular';

import { TranslateService } from '@ngx-translate/core';

import { AccountStatementService } from '../../_services/account-statement.service';
import { ContractService } from '../../_services/contract.service';
import { DialogService } from '../../_services/dialog.service';
import { SpaceService } from '../../_services/space.service';

import { AccountStatement } from '../../_models/account-statement.model';
import { Contract } from '../../_models/contract.model';
import { ButtonOptions } from '../../_enum/button-options.enum';

@Component({
  selector: 'ns-contract-information',
  templateUrl: './contract-information.component.html',
  styleUrls: ['./contract-information.component.scss'],
})
export class ContractInformationComponent implements OnInit, OnDestroy {
  private _currentContract: Contract;
  private _currentContractId: number;
  private _isBusy = false;

  private _updateContractListFlag: Subscription;
  private _firstAccountStatement: AccountStatement;

  constructor(
    protected accountStatementService: AccountStatementService,
    private contractService: ContractService,
    private dialogService: DialogService,
    private ngZone: NgZone,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private spaceService: SpaceService,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentContractId = +paramMap.get('contractId');

        this._updateContractListFlag = this.contractService.loadContractListFlag.subscribe(
          () => {
            this.getContractInformation();
          }
        );
      });
    });
  }

  ngOnDestroy() {
    this._updateContractListFlag.unsubscribe();
  }

  getContractInformation() {

    this.ngZone.run(() => {
      (async () => {
        this._firstAccountStatement = await this.accountStatementService.getFirstAccountStatement(
          this._currentContractId
        );

        this._currentContract = await this.contractService.getContract(
          this._currentContractId
        );

      })();
    });
  }

  deactivateContract() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .subscribe((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.contractService
            .setEntityStatus(this._currentContract.id, false)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.SUCCESS_MESSAGE'
                  )
                );

                this.contractService.reloadListFlag();
                this.router.back();
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  activateContract() {
    (async () => {
      const space = await this.spaceService
        .getSpace(this.currentContract.spaceId)
        .toPromise();

      // Check the availability of space attached to the contract
      if (!space.isNotOccupied) {
        this.dialogService.alert(
          this.translateService.instant(
            'CONTRACT_DETAILS_PAGE.ARCHIVED_FAILED_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'CONTRACT_DETAILS_PAGE.ARCHIVED_FAILED_DIALOG.ERROR_MESSAGE'
          )
        );
        return;
      }

      this.dialogService
        .confirm(
          this.translateService.instant(
            'CONTRACT_DETAILS_PAGE.DEARCHIVE_CONTRACT_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'CONTRACT_DETAILS_PAGE.DEARCHIVE_CONTRACT_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .subscribe((res: ButtonOptions) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this.contractService
              .setEntityStatus(this._currentContract.id, true)
              .pipe(finalize(() => (this._isBusy = false)))
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'CONTRACT_DETAILS_PAGE.DEARCHIVE_CONTRACT_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'CONTRACT_DETAILS_PAGE.DEARCHIVE_CONTRACT_DIALOG.SUCCESS_MESSAGE'
                    )
                  );

                  this.contractService.reloadListFlag();
                  this.router.back();
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'CONTRACT_DETAILS_PAGE.DEARCHIVE_CONTRACT_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'CONTRACT_DETAILS_PAGE.DEARCHIVE_CONTRACT_DIALOG.ERROR_MESSAGE'
                    )
                  );
                }
              );
          }
        });
    })();
  }

  navigateToEditContract() {
    this.router.navigate([`/contracts/${this._currentContractId}/edit`], {
      transition: {
        name: 'slideLeft',
      },
    });
  }

  encashDepositAmount(status: boolean) {
    let title = '';
    let confirmationMessage = '';
    let successMessage = '';
    let errorMessage = '';

    if (status) {
      title = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.ENCASH_DEPOSIT_AMOUNT_DIALOG.TITLE'
      );
      confirmationMessage = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.ENCASH_DEPOSIT_AMOUNT_DIALOG.CONFIRM_MESSAGE'
      );
      successMessage = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.ENCASH_DEPOSIT_AMOUNT_DIALOG.SUCCESS_MESSAGE'
      );
      errorMessage = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.ENCASH_DEPOSIT_AMOUNT_DIALOG.ERROR_MESSAGE'
      );
    } else {
      title = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.RETURN_ENCASHED_DEPOSIT_AMOUNT_DIALOG.TITLE'
      );
      confirmationMessage = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.RETURN_ENCASHED_DEPOSIT_AMOUNT_DIALOG.CONFIRM_MESSAGE'
      );
      successMessage = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.RETURN_ENCASHED_DEPOSIT_AMOUNT_DIALOG.SUCCESS_MESSAGE'
      );
      errorMessage = this.translateService.instant(
        'CONTRACT_DETAILS_PAGE.RETURN_ENCASHED_DEPOSIT_AMOUNT_DIALOG.ERROR_MESSAGE'
      );
    }

    this.dialogService
      .confirm(title, confirmationMessage)
      .subscribe((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.contractService
            .setEncashDepositAmountStatus(this._currentContract.id, status)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(title, successMessage);
                this.accountStatementService.reloadListFlag(true);
              },
              (error: Error) => {
                this.dialogService.alert(title, errorMessage);
              }
            );
        }
      });
  }

  get currentContract(): Contract {
    return this._currentContract;
  }

  get isContractArchived(): boolean {
    return Boolean(
      this.currentContract &&
        (!this._currentContract?.isActive || this._currentContract?.isExpired)
    );
  }

  get isEncashedDepositAmount(): boolean {
    return this._currentContract?.encashDepositAmount;
  }

  get isDepositAmountFullyPaid(): boolean {
    return this._firstAccountStatement?.isFullyPaid;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}
