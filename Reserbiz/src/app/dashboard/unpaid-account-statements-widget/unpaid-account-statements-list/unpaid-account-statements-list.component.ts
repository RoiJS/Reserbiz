import { Location } from '@angular/common';
import { ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { RouterExtensions } from '@nativescript/angular';

import { ObservableArray } from '@nativescript/core';
import { TranslateService } from '@ngx-translate/core';
import { delay } from 'rxjs/operators';

import { BaseListComponent } from '../../../shared/component/base-list.component';

import { AccountStatement } from '../../../_models/account-statement.model';
import { AccountStatementPaginationList } from '../../../_models/pagination_list/account-statement-pagination-list.model';

import { AccountStatementService } from '../../../_services/account-statement.service';
import { DialogService } from '../../../_services/dialog.service';
import { BaseWidgetService } from '../../../_services/base-widget.service';
import { PaymentsService } from '../../../_services/payments.service';

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
    private baseWidgetService: BaseWidgetService,
    private paymentService: PaymentsService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = accountStatementService;
  }

  ngOnInit() {
    this._loadListFlagSub = this.paymentService.loadPaymentListFlag
      .pipe(delay(2000))
      .subscribe(() => {
        this.baseWidgetService.isBusy.next(true);
        this.accountStatementService
          .getUnpaidAccountStatements()
          .subscribe(
            (
              accountStatementPaginationList: AccountStatementPaginationList
            ) => {
              this.ngZone.run(() => {
                this.baseWidgetService.listItemCount.next(
                  accountStatementPaginationList.totalItems
                );
                this.baseWidgetService.isBusy.next(false);
                this._listItems = new ObservableArray<AccountStatement>(
                  <AccountStatement[]>accountStatementPaginationList.items
                );
              });
            }
          );
      });
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
