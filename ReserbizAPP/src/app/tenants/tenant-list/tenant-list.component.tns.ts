import { Location } from '@angular/common';
import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from '@nativescript/angular';

import { IBaseListComponent } from '@src/app/_interfaces/components/ibase-list-component.interface';
import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { TenantService } from '@src/app/_services/tenant.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { Tenant } from '@src/app/_models/tenant.model';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'ns-tenant-list',
  templateUrl: './tenant-list.component.html',
  styleUrls: ['./tenant-list.component.scss'],
})
export class TenantListComponent
  extends BaseListComponent<Tenant>
  implements IBaseListComponent, OnInit, OnDestroy {
  constructor(
    protected tenantService: TenantService,
    protected dialogService: DialogService,
    protected location: Location,
    protected ngZone: NgZone,
    protected translateService: TranslateService,
    protected router: RouterExtensions
  ) {
    super(dialogService, location, ngZone, router, translateService);
    this.entityService = tenantService;
  }

  ngOnInit() {
    this._loadListFlagSub = this.tenantService.loadTenantListFlag.subscribe(
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
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deleteItemDialogTexts = {
      title: this.translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._activateItemDialogTexts = {
      title: this.translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deactivateItemDialogTexts = {
      title: this.translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
      ),
    };
  }
}
