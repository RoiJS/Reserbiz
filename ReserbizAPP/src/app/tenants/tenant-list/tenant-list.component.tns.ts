import { Location } from '@angular/common';
import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { RouterExtensions } from 'nativescript-angular/router';

import { IBaseListComponent } from '@src/app/_interfaces/ibase-list-component.interface';
import { BaseListComponent } from '@src/app/shared/component/base-list.component';

import { TenantService } from '@src/app/_services/tenant.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { Tenant } from '@src/app/_models/tenant.model';

@Component({
  selector: 'ns-tenant-list',
  templateUrl: './tenant-list.component.html',
  styleUrls: ['./tenant-list.component.scss'],
})
export class TenantListComponent extends BaseListComponent<Tenant>
  implements IBaseListComponent, OnInit, OnDestroy {
  constructor(
    private tenantService: TenantService,
    dialogService: DialogService,
    location: Location,
    ngZone: NgZone,
    translateService: TranslateService,
    router: RouterExtensions
  ) {
    super();

    this._dialogService = dialogService;
    this._entityService = tenantService;
    this._location = location;
    this._ngZone = ngZone;
    this._router = router;
    this._translateService = translateService;
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
      title: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.TITLE'
      ),
      confirmMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANTS_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deleteItemDialogTexts = {
      title: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.TITLE'
      ),
      confirmMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.REMOVE_TENANT_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._activateItemDialogTexts = {
      title: this._translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
      ),
      confirmMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.ACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
      ),
    };

    this._deactivateItemDialogTexts = {
      title: this._translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
      ),
      confirmMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this._translateService.instant(
        'TENANTS_LIST_PAGE.DEACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
      ),
    };
  }
}
