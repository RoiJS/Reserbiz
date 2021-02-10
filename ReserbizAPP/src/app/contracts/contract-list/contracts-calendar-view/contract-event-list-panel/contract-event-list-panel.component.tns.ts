import { Location } from '@angular/common';
import {
  Component,
  OnInit,
  OnDestroy,
  NgZone,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from '@nativescript/angular';
import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { Contract } from '@src/app/_models/contract.model';
import { IBaseListComponent } from '@src/app/_interfaces/ibase-list-component.interface';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { ObservableArray } from '@nativescript/core';

@Component({
  selector: 'ns-contract-event-list-panel',
  templateUrl: './contract-event-list-panel.component.html',
  styleUrls: ['./contract-event-list-panel.component.scss'],
})
export class ContractEventListPanelComponent
  extends BaseListComponent<Contract>
  implements IBaseListComponent, OnInit, OnDestroy, OnChanges {
  @Input() contractEventListItems: Contract[];

  private _openContractsCount: number;

  constructor(
    protected contractService: ContractService,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions
  ) {
    super(dialogService, location, ngZone, router, translateService);
  }

  ngOnInit() {
    this.initDialogTexts();
    super.ngOnInit();
  }

  ngOnChanges(changes: SimpleChanges) {
    this._listItems = new ObservableArray<Contract>(
      changes.contractEventListItems.currentValue
    );

    this._listItems.map((contract: Contract) => {
      contract.convertDurationBeforeContractEndsToString(this.translateService);
    });

    this._totalNumberOfItems = this._listItems.length;
    this._openContractsCount = this._listItems.filter(
      (c: Contract) => c.isOpenContract
    ).length;
  }

  initDialogTexts() {
    this._deactivateItemDialogTexts = {
      title: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'CONTRACT_LIST_PAGE.ARCHIVE_CONTRACT_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  get openContractsCount(): number {
    return this._openContractsCount;
  }
}
