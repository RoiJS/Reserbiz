import { Component, OnInit, NgZone, OnDestroy } from '@angular/core';

import { PageRoute, RouterExtensions } from '@nativescript/angular';
import { Page } from '@nativescript/core';

import { finalize } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { TranslateService } from '@ngx-translate/core';

import { TenantService } from '@src/app/_services/tenant.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { Tenant } from '@src/app/_models/tenant.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-tenant-information',
  templateUrl: './tenant-information.component.html',
  styleUrls: ['./tenant-information.component.scss'],
})
export class TenantInformationComponent implements OnInit, OnDestroy {
  private _currentTenant: Tenant;
  private _currentTenantId: number;
  private _isBusy = false;

  private _updateTenantListFlag: Subscription;

  constructor(
    private dialogService: DialogService,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private tenantService: TenantService,
    private translateService: TranslateService,
    private zone: NgZone
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentTenantId = +paramMap.get('tenantId');

        this._updateTenantListFlag = this.tenantService.loadTenantListFlag.subscribe(
          () => {
            this.getTenantInformation();
          }
        );
      });
    });
  }

  ngOnDestroy() {
    this._updateTenantListFlag.unsubscribe();
  }

  getTenantInformation() {
    this._isBusy = true;
    this.tenantService
      .getTenant(this._currentTenantId)
      .subscribe((tenant: Tenant) => {
        this._isBusy = false;
        this._currentTenant = tenant;
      });
  }

  deleteSelectedTenant(event: any) {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TENANTS_DETAILS_PAGE.REMOVE_TENANT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TENANTS_DETAILS_PAGE.REMOVE_TENANT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.tenantService
            .deleteItem(this._currentTenant.id)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.REMOVE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.REMOVE_TENANT_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.router.back();
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.REMOVE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.REMOVE_TENANT_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  activateSelectedTenant() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TENANTS_DETAILS_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TENANTS_DETAILS_PAGE.ACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.tenantService
            .setEntityStatus(this._currentTenant.id, true)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.ACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.zone.run(() => {
                      this._currentTenant.isActive = true;
                      this.tenantService.loadTenantListFlag.next();
                    });
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.ACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.ACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  deactivateSelectedTenant() {
    this.dialogService
      .confirm(
        this.translateService.instant(
          'TENANTS_DETAILS_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'TENANTS_DETAILS_PAGE.DEACTIVATE_TENANT_DIALOG.CONFIRM_MESSAGE'
        )
      )
      .then((res: ButtonOptions) => {
        if (res === ButtonOptions.YES) {
          this._isBusy = true;

          this.tenantService
            .setEntityStatus(this._currentTenant.id, false)
            .pipe(finalize(() => (this._isBusy = false)))
            .subscribe(
              () => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.DEACTIVATE_TENANT_DIALOG.SUCCESS_MESSAGE'
                  ),
                  () => {
                    this.zone.run(() => {
                      this._currentTenant.isActive = false;
                      this.tenantService.loadTenantListFlag.next();
                    });
                  }
                );
              },
              (error: Error) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.DEACTIVATE_TENANT_DIALOG.TITLE'
                  ),
                  this.translateService.instant(
                    'TENANTS_DETAILS_PAGE.DEACTIVATE_TENANT_DIALOG.ERROR_MESSAGE'
                  )
                );
              }
            );
        }
      });
  }

  get currentTenant(): Tenant {
    return this._currentTenant;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}
