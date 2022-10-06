import { Location } from '@angular/common';

import {
  Component,
  NgZone,
  OnDestroy,
  OnInit,
  ViewContainerRef,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import {
  ModalDialogOptions,
  ModalDialogService,
  RouterExtensions,
} from '@nativescript/angular';

import { BaseListComponent } from '../../shared/component/base-list.component';
import { UnitFilterDialogComponent } from './unit-filter-dialog/unit-filter-dialog.component';

import { IBaseListComponent } from '../../_interfaces/components/ibase-list-component.interface';

import { Space } from '../../_models/space.model';
import { SpaceFilter } from '../../_models/filters/space-filter.model';

import { UnitStatusEnum } from '../../_enum/unit-status.enum';

import { DialogService } from '../../_services/dialog.service';
import { SpaceService } from '../../_services/space.service';
import { StorageService } from '../../_services/storage.service';

@Component({
  selector: 'app-space-list',
  templateUrl: './space-list.component.html',
  styleUrls: ['./space-list.component.scss'],
})
export class SpaceListComponent
  extends BaseListComponent<Space>
  implements IBaseListComponent, OnInit, OnDestroy
{
  private UNIT_FILTER_STATUS = 'UnitFilter_status';
  private UNIT_FILTER_UNIT_TYPE = 'UnitFilter_unitType';

  constructor(
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected router: RouterExtensions,
    protected translateService: TranslateService,
    private spaceService: SpaceService,
    private storageService: StorageService,
    private modalDialogService: ModalDialogService,
    private vcRef: ViewContainerRef
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = spaceService;

    this._entityFilter = new SpaceFilter();
    this._entityFilter.page = 1;
  }

  ngOnInit() {
    this._loadListFlagSub = this.spaceService.loadSpacesFlag.subscribe(() => {
      this.initFilterOptions();
      this.getPaginatedEntities();
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

  openFilterDialog() {
    this.initFilterDialog().then(
      (data: { filterHasChanged: boolean; filter: SpaceFilter }) => {
        if (!data) {
          return;
        }

        if (!data.filterHasChanged) {
          return;
        }

        if (data.filter.isFilterActive()) {
          this.storeFilterOptions(data.filter);
        } else {
          (<SpaceFilter>this._entityFilter).reset();
          this.resetFilterOptions();
        }

        // Needs to reset the page number to get
        // the correct data
        this._entityFilter.page = 1;
        this.entityService.reloadListFlag();
      }
    );
  }

  private initFilterOptions() {
    const status = this.storageService.getString(`${this.UNIT_FILTER_STATUS}`);
    const unitType = this.storageService.getString(
      `${this.UNIT_FILTER_UNIT_TYPE}`
    );

    if (status) {
      (<SpaceFilter>this._entityFilter).status = <UnitStatusEnum>(
        parseInt(status)
      );
    }

    if (unitType) {
      (<SpaceFilter>this._entityFilter).unitTypeId = parseInt(unitType);
    }
  }

  private storeFilterOptions(spaceFilter: SpaceFilter) {
    const status = spaceFilter.status;
    const unitType = spaceFilter.unitTypeId;

    if (status) {
      this.storageService.storeString(
        `${this.UNIT_FILTER_STATUS}`,
        status.toString()
      );
    }

    if (unitType) {
      this.storageService.storeString(
        `${this.UNIT_FILTER_UNIT_TYPE}`,
        unitType.toString()
      );
    }
  }

  private resetFilterOptions() {
    this.storageService.remove(`${this.UNIT_FILTER_STATUS}`);
    this.storageService.remove(`${this.UNIT_FILTER_UNIT_TYPE}`);
  }

  private initFilterDialog(): Promise<any> {
    const dialogOptions: ModalDialogOptions = {
      viewContainerRef: this.vcRef,
      context: {
        spaceFilter: <SpaceFilter>this._entityFilter,
      },
      fullscreen: false,
      animated: true,
    };

    return this.modalDialogService.showModal(
      UnitFilterDialogComponent,
      dialogOptions
    );
  }
}
