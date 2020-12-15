import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { PageRoute, RouterExtensions } from 'nativescript-angular/router';

import { TranslateService } from '@ngx-translate/core';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceService } from '@src/app/_services/space.service';

import { Contract } from '@src/app/_models/contract.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

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

  constructor(
    private contractService: ContractService,
    private dialogService: DialogService,
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
    this._isBusy = true;

    (async () => {
      this._currentContract = await this.contractService.getContract(
        this._currentContractId
      );

      this._isBusy = false;
    })();
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
      .then((res: ButtonOptions) => {
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
        .then((res: ButtonOptions) => {
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

  get currentContract(): Contract {
    return this._currentContract;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}
