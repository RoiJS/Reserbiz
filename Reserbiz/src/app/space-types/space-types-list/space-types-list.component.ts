import { Location } from '@angular/common';
import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from '@nativescript/angular';

import { BaseListComponent } from '../../shared/component/base-list.component';

import { IBaseListComponent } from '../../_interfaces/components/ibase-list-component.interface';

import { SpaceTypeService } from '../../_services/space-type.service';
import { DialogService } from '../../_services/dialog.service';

import { SpaceType } from '../../_models/space-type.model';

@Component({
  selector: 'ns-space-types-list',
  templateUrl: './space-types-list.component.html',
  styleUrls: ['./space-types-list.component.scss'],
})
export class SpaceTypesListComponent
  extends BaseListComponent<SpaceType>
  implements IBaseListComponent, OnInit, OnDestroy {
  constructor(
    private spaceTypeService: SpaceTypeService,
    dialogService: DialogService,
    location: Location,
    ngZone: NgZone,
    router: RouterExtensions,
    translateService: TranslateService
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = spaceTypeService;
  }

  ngOnInit() {
    this._loadListFlagSub = this.spaceTypeService.loadSpaceTypesFlag.subscribe(
      () => {
        this.getEntities();
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
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPES_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deleteItemDialogTexts = {
      title: this.translateService.instant(
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'SPACE_TYPE_LIST_PAGE.REMOVE_SPACE_TYPE_DIALOG.ERROR_MESSAGE'
      ),
    };
  }
}
