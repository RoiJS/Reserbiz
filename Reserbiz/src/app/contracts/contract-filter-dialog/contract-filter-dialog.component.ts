import { Component, OnInit, AfterViewInit, ViewChild } from "@angular/core";

import { TranslateService } from "@ngx-translate/core";

import { ModalDialogParams } from "@nativescript/angular";
import { RadDataFormComponent } from "nativescript-ui-dataform/angular";

import { TenantService } from "~/app/_services/tenant.service";

import { TenantOption } from "~/app/_models/options/tenant-option.model";
import { ContractFilter } from "~/app/_models/filters/contract-filter.model";
import { ContractFilterFormSource } from "~/app/_models/form/contract-filter-form.model";
import { SortOrderEnum } from "~/app/_enum/sort-order.enum";

import { TenantValueProvider } from "~/app/_helpers/value_providers/tenant-value-provider.helper";
import { SortOrderValueProvider } from "~/app/_helpers/value_providers/sort-order-value-provider.helper";
import { BaseFormHelper } from "~/app/_helpers/base_helpers/base-form.helper";
import { YesNoValueProvider } from "~/app/_helpers/value_providers/yesno-value-provider.helper";
import { YesNoEnum } from "~/app/_enum/yesno-unit.enum";

@Component({
  selector: "ns-contract-filter-dialog",
  templateUrl: "./contract-filter-dialog.component.html",
  styleUrls: ["./contract-filter-dialog.component.scss"],
})
export class ContractFilterDialogComponent
  extends BaseFormHelper<ContractFilterFormSource>
  implements OnInit, AfterViewInit
{
  @ViewChild(RadDataFormComponent, { static: false })
  contractFilterForm: RadDataFormComponent;

  private _contractFilterData: ContractFilter;
  private _contractFilterFormSource: ContractFilterFormSource;
  private _contractFilterFormSourceOriginal: ContractFilterFormSource;

  private _tenantValueProvider: TenantValueProvider;
  private _sortOrderValueProvider: SortOrderValueProvider;
  private _yesNoValueProvider: YesNoValueProvider;
  private _tenantOptions;

  constructor(
    private params: ModalDialogParams,
    private tenantService: TenantService,
    private translateService: TranslateService
  ) {
    super();
    this._contractFilterData = <ContractFilter>params.context.contractFilter;
    this._contractFilterFormSource = new ContractFilterFormSource(
      this._contractFilterData.tenantId,
      this._contractFilterData.activeFromFilter,
      this._contractFilterData.activeToFilter,
      this._contractFilterData.nextDueDateFromFilter,
      this._contractFilterData.nextDueDateToFilter,
      this._contractFilterData.openContract ? YesNoEnum.Yes : YesNoEnum.No,
      this._contractFilterData.sortOrder
    );

    this._contractFilterFormSourceOriginal =
      this._contractFilterFormSource.clone();
    this.initFilterOptions(this._contractFilterData);
  }

  ngOnInit() {
    this._tenantValueProvider = new TenantValueProvider(
      this.translateService,
      this.tenantService
    );

    this._sortOrderValueProvider = new SortOrderValueProvider(
      this.translateService
    );

    this._yesNoValueProvider = new YesNoValueProvider(this.translateService);

    this._tenantOptions = this._tenantValueProvider.tenantOptions;
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this._tenantValueProvider.setCurrenValue(
        this._contractFilterFormSource.tenantId
      );

      this._tenantOptions = this._tenantValueProvider.tenantOptions;
    }, 500);
  }

  initFilterOptions(contractFilter: ContractFilter) {
    if (contractFilter.tenantId) {
      this._contractFilterFormSource.tenantId = Number(contractFilter.tenantId);
    }

    if (contractFilter.activeFromFilter) {
      this._contractFilterFormSource.activeFrom = new Date(
        contractFilter.activeFromFilter
      );
    }

    if (contractFilter.activeToFilter) {
      this._contractFilterFormSource.activeTo = new Date(
        contractFilter.activeToFilter
      );
    }

    if (contractFilter.nextDueDateFromFilter) {
      this._contractFilterFormSource.nextDueDateFrom = new Date(
        contractFilter.nextDueDateFromFilter
      );
    }

    if (contractFilter.nextDueDateToFilter) {
      this._contractFilterFormSource.nextDueDateTo = new Date(
        contractFilter.nextDueDateToFilter
      );
    }

    if (contractFilter.openContract) {
      this._contractFilterFormSource.openContract = Number(
        contractFilter.openContract ? YesNoEnum.Yes : YesNoEnum.No
      );
    }

    if (contractFilter.sortOrder) {
      this._contractFilterFormSource.sortOrder = contractFilter.sortOrder;
    }
  }

  onConfirm() {
    const filterHasChanged = !this._contractFilterFormSource.isSame(
      this._contractFilterFormSourceOriginal
    );
    if (filterHasChanged) {
      this.setFilterValues();
    }
    this.params.closeCallback({
      filterHasChanged: filterHasChanged,
      filter: this._contractFilterData,
    });
  }

  onReset() {
    this.resetFilterValues();
  }

  setFilterValues() {
    this._contractFilterData.tenantId = this._contractFilterFormSource.tenantId;
    this._contractFilterData.activeFromFilter =
      this._contractFilterFormSource.activeFrom;
    this._contractFilterData.activeToFilter =
      this._contractFilterFormSource.activeTo;
    this._contractFilterData.nextDueDateFromFilter =
      this._contractFilterFormSource.nextDueDateFrom;
    this._contractFilterData.nextDueDateToFilter =
      this._contractFilterFormSource.nextDueDateTo;
    this._contractFilterData.openContract =
      this._contractFilterFormSource.openContract === YesNoEnum.Yes;
    this._contractFilterData.sortOrder =
      this._contractFilterFormSource.sortOrder;
  }

  resetFilterValues() {
    this._contractFilterFormSource = this.reloadFormSource(
      this._contractFilterFormSource,
      <ContractFilterFormSource>{
        tenantId: null,
        activeFrom: null,
        activeTo: null,
        nextDueDateFrom: null,
        nextDueDateTo: null,
        openContract: YesNoEnum.No,
        sortOrder: SortOrderEnum.Ascending,
      }
    );
  }

  closeDialog() {
    this.params.closeCallback();
  }

  get contractFilterFormSource() {
    return this._contractFilterFormSource;
  }

  get tenantOptions(): {
    key: string;
    label: string;
    items: TenantOption[];
  } {
    return this._tenantOptions;
  }

  get sortOrderOptions(): { key: SortOrderEnum; label: string }[] {
    return this._sortOrderValueProvider?.sortOrderOptions;
  }

  get yesNoOptions(): { key: YesNoEnum; label: string }[] {
    return this._yesNoValueProvider?.yesNoOptions;
  }
}
