import { Location } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  NgZone,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { delay } from 'rxjs/operators';

import { PageRoute, RouterExtensions } from '@nativescript/angular';

import { BaseListComponent } from '../../../../../shared/component/base-list.component';

import { PenaltyFilter } from '../../../../../_models/filters/penalty-filter.model';
import { Penalty } from '../../../../../_models/penalty.model';
import { PenaltyPaginationList } from '../../../../../_models/pagination_list/penalty-pagination-list.model';

import { SortOrderEnum } from '../../../../../_enum/sort-order.enum';

import { DialogService } from '../../../../../_services/dialog.service';
import { PenaltyService } from '../../../../../_services/penalty.service';

import { NumberFormatter } from '../../../../../_helpers/formatters/number-formatter.helper';

@Component({
  selector: 'app-penalties',
  templateUrl: './penalties.component.html',
  styleUrls: ['./penalties.component.scss'],
})
export class PenaltiesComponent
  extends BaseListComponent<Penalty>
  implements OnInit, OnDestroy {
  private _totalAmount = 0;

  constructor(
    protected penaltyService: PenaltyService,
    protected changeDetectorRef: ChangeDetectorRef,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions,
    private pageRoute: PageRoute
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = penaltyService;
    this.changeDetectorRef = changeDetectorRef;

    // take note to override the filter from the
    // base component list if derived component
    // class need to support more complex
    // filter feature.
    this._entityFilter = new PenaltyFilter();
    this._entityFilter.page = 1;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentItemParentId = +paramMap.get('accountStatementId');

        this._loadListFlagSub = this.penaltyService.loadPenaltyListFlag.subscribe(
          (reset: boolean) => {
            (<PenaltyFilter>(
              this._entityFilter
            )).parentId = this._currentItemParentId;
            this.getPaginatedEntities(
              (penaltyPaginationList: PenaltyPaginationList) => {
                this._totalAmount = penaltyPaginationList.totalAmount;
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

  setSortOrder(sortOrder: SortOrderEnum) {
    this._entityFilter.sortOrder = <SortOrderEnum>sortOrder;
    this.penaltyService.loadPenaltyListFlag.next(true);
  }

  openPenaltyDetailsDialog(paymentItemIndex: number) {
    setTimeout(() => {
      // Override default behavior when selecting item from the list
      this.appListView.listView.deselectItemAt(paymentItemIndex);
    }, 100);
  }

  get currentSortOrder(): SortOrderEnum {
    return this._entityFilter.sortOrder;
  }

  get totalAmount(): string {
    return NumberFormatter.formatCurrency(this._totalAmount);
  }
}
