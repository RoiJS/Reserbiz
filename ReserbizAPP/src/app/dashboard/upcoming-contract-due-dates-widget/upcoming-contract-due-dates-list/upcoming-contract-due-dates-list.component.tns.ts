import { Location } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  NgZone,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { RouterExtensions } from '@nativescript/angular';

import { ObservableArray } from '@nativescript/core';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { ContractPaginationList } from '@src/app/_models/contract-pagination-list.model';
import { Contract } from '@src/app/_models/contract.model';

import { ContractService } from '@src/app/_services/contract.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { UpcomingContractDueDatesWidgetService } from '@src/app/_services/upcoming-contract-due-dates-widget.service';

@Component({
  selector: 'ns-upcoming-contract-due-dates-list',
  templateUrl: './upcoming-contract-due-dates-list.component.html',
  styleUrls: [
    './upcoming-contract-due-dates-list.component.scss',
    '../../../shared/styles/base-widget.scss',
  ],
})
export class UpcomingContractDueDatesListComponent
  extends BaseListComponent<Contract>
  implements OnInit, OnDestroy {
  constructor(
    protected contractService: ContractService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private upcomingContractsDueDateService: UpcomingContractDueDatesWidgetService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = contractService;
  }

  ngOnInit() {
    this._loadListFlagSub = this.upcomingContractsDueDateService.selectedMonth.subscribe(
      (month: number) => {
        this.upcomingContractsDueDateService.isBusy.next(true);
        this.contractService
          .getAllUpcomingDueDateContractsPerMonth(month)
          .subscribe((contractPaginationList: ContractPaginationList) => {
            this.upcomingContractsDueDateService.listItemCount.next(
              contractPaginationList.totalItems
            );
            this.upcomingContractsDueDateService.isBusy.next(false);
            this._listItems = new ObservableArray<Contract>(
              <Contract[]>contractPaginationList.items
            );
          });
      }
    );
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }
}
