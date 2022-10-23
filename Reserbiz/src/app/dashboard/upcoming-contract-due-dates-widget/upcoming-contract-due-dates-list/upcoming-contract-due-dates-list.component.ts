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

import { BaseListComponent } from '../../../shared/component/base-list.component';

import { ContractPaginationList } from '../../../_models/pagination_list/contract-pagination-list.model';
import { Contract } from '../../../_models/contract.model';

import { ContractService } from '../../../_services/contract.service';
import { DialogService } from '../../../_services/dialog.service';
import { UpcomingContractDueDatesWidgetService } from '../../../_services/upcoming-contract-due-dates-widget.service';

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
        setTimeout(() => {
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
        }, 2000);
      }
    );
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }
}
