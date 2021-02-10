import { Location } from '@angular/common';

import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { RouterExtensions } from '@nativescript/angular';

import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { IBaseListComponent } from '@src/app/_interfaces/ibase-list-component.interface';

import { Space } from '@src/app/_models/space.model';
import { SpaceFilter } from '@src/app/_models/space-filter.model';

import { DialogService } from '@src/app/_services/dialog.service';
import { SpaceService } from '@src/app/_services/space.service';

@Component({
  selector: 'app-space-list',
  templateUrl: './space-list.component.html',
  styleUrls: ['./space-list.component.scss'],
})
export class SpaceListComponent
  extends BaseListComponent<Space>
  implements IBaseListComponent, OnInit, OnDestroy {
  constructor(
    private spaceService: SpaceService,
    dialogService: DialogService,
    location: Location,
    ngZone: NgZone,
    router: RouterExtensions,
    translateService: TranslateService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = spaceService;

    this._entityFilter = new SpaceFilter();
    this._entityFilter.page = 1;
  }

  ngOnInit() {
    this._loadListFlagSub = this.spaceService.loadSpacesFlag.subscribe(
      () => {
        this.getPaginatedEntities();
      }
    );

    this.initDialogTexts();
    super.ngOnInit();
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  initDialogTexts() {
    this._deleteMultipleItemsDialogTexts = {
      title: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACES_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACES_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACES_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACES_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deleteItemDialogTexts = {
      title: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACE_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACE_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACE_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SPACE_LIST_PAGE.REMOVE_SPACE_DIALOG.ERROR_MESSAGE'
      ),
    };
  }
}
