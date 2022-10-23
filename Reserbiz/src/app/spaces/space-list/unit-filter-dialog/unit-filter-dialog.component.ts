import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ModalDialogParams } from '@nativescript/angular';
import { TranslateService } from '@ngx-translate/core';
import { UnitStatusEnum } from '../../../_enum/unit-status.enum';
import { BaseFormHelper } from '../../../_helpers/base_helpers/base-form.helper';

import { SpaceTypeValueProvider } from '../../../_helpers/value_providers/space-type-value-provider.helper';
import { UnitStatusValueProvider } from '../../../_helpers/value_providers/unit-status-value-provider.helper';

import { SpaceFilter } from '../../../_models/filters/space-filter.model';
import { UnitFilterFormSource } from '../../../_models/form/unit-filter-form.model';
import { SpaceTypeOption } from '../../../_models/options/space-type-option.model';

import { SpaceTypeService } from '../../../_services/space-type.service';

@Component({
  selector: 'app-unit-filter-dialog',
  templateUrl: './unit-filter-dialog.component.html',
  styleUrls: ['./unit-filter-dialog.component.scss'],
})
export class UnitFilterDialogComponent
  extends BaseFormHelper<UnitFilterFormSource>
  implements OnInit, AfterViewInit
{
  private _unitFilterData: SpaceFilter;
  private _unitFilterFormSource: UnitFilterFormSource;
  private _unitFilterFormSourceOriginal: UnitFilterFormSource;

  private _unitStatusValueProvider: UnitStatusValueProvider;
  private _unitTypeValueProvider: SpaceTypeValueProvider;
  private _unitTypeOptions;

  constructor(
    private params: ModalDialogParams,
    private spaceTypeService: SpaceTypeService,
    private translateService: TranslateService
  ) {
    super();
    this._unitFilterData = <SpaceFilter>params.context.spaceFilter;
    this._unitFilterFormSource = new UnitFilterFormSource(
      this._unitFilterData.status,
      this._unitFilterData.unitTypeId
    );

    this._unitFilterFormSourceOriginal = this._unitFilterFormSource.clone();
    this.initFilterOptions(this._unitFilterData);
  }

  ngOnInit() {
    this._unitStatusValueProvider = new UnitStatusValueProvider(
      this.translateService
    );

    this._unitTypeValueProvider = new SpaceTypeValueProvider(
      this.translateService,
      this.spaceTypeService,
      true
    );
    this._unitTypeOptions = this._unitTypeValueProvider.spaceTypeOptions;
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this._unitTypeValueProvider.setCurrenValue(
        this._unitFilterFormSource.unitTypeId
      );

      this._unitTypeOptions = this._unitTypeValueProvider.spaceTypeOptions;
    }, 500);
  }

  initFilterOptions(unitFilter: SpaceFilter) {
    if (unitFilter.status) {
      this._unitFilterFormSource.status = unitFilter.status;
    }

    if (unitFilter.unitTypeId) {
      this._unitFilterFormSource.unitTypeId = Number(unitFilter.unitTypeId);
    }
  }

  onConfirm() {
    const filterHasChanged = !this._unitFilterFormSource.isSame(
      this._unitFilterFormSourceOriginal
    );
    if (filterHasChanged) {
      this.setFilterValues();
    }
    this.params.closeCallback({
      filterHasChanged: filterHasChanged,
      filter: this._unitFilterData,
    });
  }

  onReset() {
    this.resetFilterValues();
  }

  setFilterValues() {
    this._unitFilterData.status = this._unitFilterFormSource.status;
    this._unitFilterData.unitTypeId = this._unitFilterFormSource.unitTypeId;
  }

  resetFilterValues() {
    this._unitFilterFormSource = this.reloadFormSource(
      this._unitFilterFormSource,
      <UnitFilterFormSource>{
        status: UnitStatusEnum.All,
        unitTypeId: null,
      }
    );
  }

  closeDialog() {
    this.params.closeCallback();
  }

  get unitFilterFormSource() {
    return this._unitFilterFormSource;
  }

  get unitTypeOptions(): {
    key: string;
    label: string;
    items: SpaceTypeOption[];
  } {
    return this._unitTypeOptions;
  }

  get unitStatusOptions(): Array<{ key: UnitStatusEnum; label: string }> {
    return this._unitStatusValueProvider.statusOptions;
  }
}
