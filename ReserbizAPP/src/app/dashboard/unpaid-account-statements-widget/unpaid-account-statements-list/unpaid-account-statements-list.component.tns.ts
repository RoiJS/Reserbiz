import { Location } from '@angular/common';
import { ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { RouterExtensions } from 'nativescript-angular';

import { ObservableArray } from 'tns-core-modules/data/observable-array';
import { TranslateService } from '@ngx-translate/core';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { AccountStatement } from '@src/app/_models/account-statement.model';
import { AccountStatementPaginationList } from '@src/app/_models/account-statement-pagination-list.model';

import { AccountStatementService } from '@src/app/_services/account-statement.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { BaseWidgetService } from '@src/app/_services/base-widget.service';

@Component({
  selector: 'ns-unpaid-account-statements-list',
  templateUrl: './unpaid-account-statements-list.component.html',
  styleUrls: [
    './unpaid-account-statements-list.component.scss',
    '../../../shared/styles/base-widget.scss',
  ],
})
export class UnpaidAccountStatementsListComponent
  extends BaseListComponent<AccountStatement>
  implements OnInit {
  constructor(
    protected accountStatementService: AccountStatementService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private baseWidgetService: BaseWidgetService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = accountStatementService;
  }

  ngOnInit() {
    this.baseWidgetService.isBusy.next(true);
    this.accountStatementService
      .getUnpaidAccountStatements()
      .subscribe(
        (accountStatementPaginationList: AccountStatementPaginationList) => {
          this.baseWidgetService.listItemCount.next(
            accountStatementPaginationList.totalItems
          );
          this.baseWidgetService.isBusy.next(false);
          this._listItems = new ObservableArray<AccountStatement>(
            <AccountStatement[]>accountStatementPaginationList.items
          );
        }
      );
  }

  selectItem(currentIndex: number, url: string) {
    const selectedItem = <AccountStatement>(
      this._listItems.getItem(currentIndex)
    );

    this.navigateToOtherPage(
      url
        .replace(':contractId', selectedItem.contractId.toString())
        .replace(':accountStatementId', selectedItem.id.toString())
    );
  }
}
