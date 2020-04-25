import { Location } from '@angular/common';
import { Component, OnInit, NgZone, OnDestroy } from '@angular/core';

import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { TranslateService } from '@ngx-translate/core';

import { finalize } from 'rxjs/operators';

import { TenantService } from '@src/app/_services/tenant.service';
import { DialogService } from '@src/app/_services/dialog.service';

import { Tenant } from '@src/app/_models/tenant.model';

import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'ns-tenant-details',
  templateUrl: './tenant-details-tab.component.html',
  styleUrls: ['./tenant-details-tab.component.scss'],
})
export class TenantDetailsTabComponent implements OnInit, OnDestroy {
  private _currentTenant: Tenant;
  private _isBusy = false;

  private _locationSub: any;

  constructor(
    private dialogService: DialogService,
    private location: Location,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private tenantService: TenantService,
    private translateService: TranslateService,
    private active: ActivatedRoute,
    private zone: NgZone
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const tenantId = +paramMap.get('tenantId');

        this.getTenantInformation(tenantId);
        this.loadTabRoutes(tenantId);
      });
    });

    this._locationSub = this.location.subscribe(() => {
      this.getTenantInformation(this._currentTenant.id);
    });
  }

  ngOnDestroy() {
    this._locationSub.unsubscribe();
  }

  loadTabRoutes(tenantId: number) {
    setTimeout(() => {
      this.router.navigate(
        [
          {
            outlets: {
              contractList: ['contractList', tenantId],
            },
          },
        ],
        { relativeTo: this.active }
      );
    }, 10);
  }

  getTenantInformation(tenantId: number) {
    this._isBusy = true;
    this.tenantService.getTenant(tenantId).subscribe((tenant: Tenant) => {
      this._isBusy = false;
      this._currentTenant = tenant;
    });
  }

  navigateBack() {
    this.router.back({
      outlets: null,
      relativeTo: this.active,
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
            .deleteTenant(this._currentTenant.id)
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
            .setTenantStatus(this._currentTenant.id, true)
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
            .setTenantStatus(this._currentTenant.id, false)
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
