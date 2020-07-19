import { Location } from '@angular/common';
import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { Contract } from '@src/app/_models/contract.model';
import { IBaseListComponent } from '@src/app/_interfaces/ibase-list-component.interface';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from 'nativescript-angular/router';

@Component({
  selector: 'ns-contract-list',
  templateUrl: './contract-list.component.html',
  styleUrls: ['./contract-list.component.scss'],
})
export class ContractListComponent extends BaseListComponent<Contract>
  implements IBaseListComponent, OnInit, OnDestroy {
  constructor(
    protected contractService: ContractService,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = contractService;
  }

  ngOnInit() {
    this._loadListFlagSub = this.contractService.loadContractListFlag.subscribe(
      () => {
        this.getEntities(null, () => {
          this._listItems.map((contract: Contract) => {
            contract.convertDurationBeforeContractEndsToString(
              this.translateService
            );
          });
        });
      }
    );

    this.initDialogTexts();
    super.ngOnInit();
  }

  initDialogTexts() {}

  ngOnDestroy() {
    super.ngOnDestroy();
  }
}
