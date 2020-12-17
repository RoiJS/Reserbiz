import { Location } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  NgZone,
  OnDestroy,
  OnInit,
  ViewContainerRef,
} from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import {
  ModalDialogService,
  PageRoute,
  RouterExtensions,
} from 'nativescript-angular';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { Payment } from '@src/app/_models/payment.model';

import { DialogService } from '@src/app/_services/dialog.service';
import { PaymentsService } from '@src/app/_services/payments.service';
import { PaymentFilter } from '@src/app/_models/payment-filter.model';
import { PaymentPaginationList } from '@src/app/_models/payment-pagination-list.model';
import { SortOrderEnum } from '@src/app/_enum/sort-order.enum';

@Component({
  selector: 'ns-payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.scss'],
})
export class PaymentsComponent
  extends BaseListComponent<Payment>
  implements OnInit, OnDestroy {
  constructor(
    protected paymentService: PaymentsService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private modalDialogService: ModalDialogService,
    private vcRef: ViewContainerRef,
    private pageRoute: PageRoute
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = paymentService;
    this.changeDetectorRef = changeDetectorRef;

    // take note to override the filter from the
    // base component list if derived component
    // class need to support more complex
    // filter feature.
    this._entityFilter = new PaymentFilter();
    this._entityFilter.page = 1;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentItemParentId = +paramMap.get('accountStatementId');

        console.log(this._currentItemParentId);

        this._loadListFlagSub = this.paymentService.loadPaymentListFlag.subscribe(
          () => {
            this._entityFilter.parentId = this._currentItemParentId;
            this.getPaginatedEntities(
              (paymentPaginationList: PaymentPaginationList) => {
                console.log(paymentPaginationList.items);
              }
            );
          }
        );
      });
    });

    super.ngOnInit();
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  openAddPaymentDialog() {
    console.log('Open add payment dialog here!');
  }

  openPaymentDetailsDialog(paymentItemIndex: number) {
    const paymentDetails = <Payment>this._listItems.getItem(paymentItemIndex);

    setTimeout(() => {
      // Override default behavior when selecting item from the list
      this.appListView.listView.deselectItemAt(paymentItemIndex);
    }, 100);

    console.log(
      `Open view payment details dialog here! payment details: ${paymentDetails}`
    );
  }

  setSortOrder(sortOrder: SortOrderEnum) {
    this._entityFilter.sortOrder = <SortOrderEnum>sortOrder;
    this.paymentService.loadPaymentListFlag.next();

    console.log(`Current sort order ${sortOrder}`);
  }

  get currentSortOrder(): SortOrderEnum {
    return this._entityFilter.sortOrder;
  }
}
