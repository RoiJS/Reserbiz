import { Location } from '@angular/common';
import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';

import { delay } from 'rxjs/operators';

import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from '@nativescript/angular';

import { TermService } from '../../_services/term.service';
import { DialogService } from '../../_services/dialog.service';

import { Term } from '../../_models/term.model';

import { IBaseListComponent } from '../../_interfaces/components/ibase-list-component.interface';

import { BaseListComponent } from '../../shared/component/base-list.component';

@Component({
  selector: 'ns-terms-list',
  templateUrl: './terms-list.component.html',
  styleUrls: ['./terms-list.component.scss'],
})
export class TermsListComponent
  extends BaseListComponent<Term>
  implements IBaseListComponent, OnInit, OnDestroy {
  constructor(
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected termService: TermService,
    protected router: RouterExtensions
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = termService;
  }

  ngOnInit() {
    this._loadListFlagSub = this.termService.loadTermListFlag.subscribe(() => {
      this.getEntities();
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
        'TERMS_LIST_PAGE.REMOVE_TERMS_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.REMOVE_TERMS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.REMOVE_TERMS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.REMOVE_TERMS_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deleteItemDialogTexts = {
      title: this.translateService.instant(
        'TERMS_LIST_PAGE.REMOVE_TERM_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.REMOVE_TERM_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.REMOVE_TERM_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.REMOVE_TERM_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._activateItemDialogTexts = {
      title: this.translateService.instant(
        'TERMS_LIST_PAGE.ACTIVATE_TERM_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.ACTIVATE_TERM_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.ACTIVATE_TERM_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.ACTIVATE_TERM_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deactivateItemDialogTexts = {
      title: this.translateService.instant(
        'TERMS_LIST_PAGE.DEACTIVATE_TERM_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.DEACTIVATE_TERM_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.DEACTIVATE_TERM_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TERMS_LIST_PAGE.DEACTIVATE_TERM_DIALOG.ERROR_MESSAGE'
      ),
    };
  }
}
