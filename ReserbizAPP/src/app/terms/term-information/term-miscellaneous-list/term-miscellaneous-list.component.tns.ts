import { Location } from '@angular/common';
import { Component, OnInit, NgZone, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RouterExtensions, PageRoute } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';

import { DialogService } from '@src/app/_services/dialog.service';
import { TermMiscellaneousService } from '@src/app/_services/term-miscellaneous.service';
import { IBaseListComponent } from '@src/app/_interfaces/components/ibase-list-component.interface';
import { BaseListComponent } from '@src/app/shared/component/base-list.component';
import { TermMiscellaneous } from '@src/app/_models/term-miscellaneous.model';

@Component({
  selector: 'ns-term-miscellaneous-list',
  templateUrl: './term-miscellaneous-list.component.html',
  styleUrls: ['./term-miscellaneous-list.component.scss'],
})
export class TermMiscellaneousListComponent
  extends BaseListComponent<TermMiscellaneous>
  implements IBaseListComponent, OnInit, OnDestroy {
  constructor(
    private pageRoute: PageRoute,
    private termMiscellaneousService: TermMiscellaneousService,
    activatedRoute: ActivatedRoute,
    dialogService: DialogService,
    ngZone: NgZone,
    location: Location,
    router: RouterExtensions,
    translateService: TranslateService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.activatedRoute = activatedRoute;
    this.entityService = termMiscellaneousService;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentItemParentId = +paramMap.get('termId');

        this._loadListFlagSub = this.termMiscellaneousService.loadTermMiscellaneousListFlag.subscribe(
          () => {
            this._entityFilter.parentId = this._currentItemParentId;
            this.getEntities();
          }
        );
      });
    });

    this.initDialogTexts();
    super.ngOnInit();
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  initDialogTexts() {
    this._deleteMultipleItemsDialogTexts = {
      title: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deleteItemDialogTexts = {
      title: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERM_MISCELLANEOUS_LIST_PAGE.REMOVE_TERM_MISCELLANEOUS_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  goToAddTermMiscellaneousPage() {
    this.navigateToOtherPage(`${this._currentItemParentId}/add`);
  }

  goToEditDetailsFromOptionItem() {
    this.navigateToOtherPage(this._currentItem.id.toString());
  }
}
