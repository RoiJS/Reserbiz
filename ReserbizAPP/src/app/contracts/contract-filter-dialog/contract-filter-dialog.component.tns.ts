import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { ModalDialogParams } from 'nativescript-angular/modal-dialog';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';

import { TenantService } from '@src/app/_services/tenant.service';
import { TenantOption } from '@src/app/_models/tenant-option.model';
import { ContractFilter } from '@src/app/_models/contract-filter.model';
import { TenantValueProvider } from '@src/app/_helpers/tenant-value-provider.helper';
import { FilterOptionEnum } from '@src/app/_enum/filter-option.enum';

@Component({
  selector: 'ns-contract-filter-dialog',
  templateUrl: './contract-filter-dialog.component.html',
  styleUrls: ['./contract-filter-dialog.component.scss'],
})
export class ContractFilterDialogComponent implements OnInit, AfterViewInit {
  @ViewChild(RadDataFormComponent, { static: false })
  contractFilterForm: RadDataFormComponent;

  private _contractFilterData: ContractFilter;

  private _tenantValueProvider: TenantValueProvider;
  private _tenantOptions;

  constructor(
    private params: ModalDialogParams,
    private tenantService: TenantService,
    private translateService: TranslateService
  ) {
    this._contractFilterData = new ContractFilter();
    this.initFilterOptions(params.context.contractFilter);
  }

  ngOnInit() {
    this._tenantValueProvider = new TenantValueProvider(
      this.translateService,
      this.tenantService
    );
    this._tenantOptions = this._tenantValueProvider.tenantOptions;
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this._tenantValueProvider.setCurrenValue(
        this._contractFilterData.tenantId
      );

      this._tenantOptions = this._tenantValueProvider.tenantOptions;
    }, 500);
  }

  initFilterOptions(contractFilter: ContractFilter) {
    if (contractFilter.tenantId) {
      this._contractFilterData.tenantId = Number(contractFilter.tenantId);
    }

    if (contractFilter.activeFrom) {
      this._contractFilterData.activeFrom = new Date(contractFilter.activeFrom);
    }

    if (contractFilter.activeTo) {
      this._contractFilterData.activeTo = new Date(contractFilter.activeTo);
    }

    if (contractFilter.nextDueDateFrom) {
      this._contractFilterData.nextDueDateFrom = new Date(
        contractFilter.nextDueDateFrom
      );
    }

    if (contractFilter.nextDueDateTo) {
      this._contractFilterData.nextDueDateTo = new Date(
        contractFilter.nextDueDateTo
      );
    }

    if (contractFilter.openContract) {
      this._contractFilterData.openContract = Boolean(
        contractFilter.openContract
      );
    }
  }

  onConfirm() {
    this.params.closeCallback({
      result: FilterOptionEnum.Confirm,
      filter: this._contractFilterData,
    });
  }

  onReset() {
    this._contractFilterData.reset();
    this.params.closeCallback({
      result: FilterOptionEnum.Reset,
      filter: this._contractFilterData,
    });
  }

  closeDialog() {
    this.params.closeCallback();
  }

  get contractFilterData() {
    return this._contractFilterData;
  }

  get tenantOptions(): {
    key: string;
    label: string;
    items: TenantOption[];
  } {
    return this._tenantOptions;
  }
}
