import { Component, OnInit, ViewChild } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';

import { finalize } from 'rxjs/operators';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { TenantService } from '@src/app/_services/tenant.service';
import { DialogService } from '@src/app/_services/dialog.service';

import { Tenant } from '@src/app/_models/tenant.model';
import { TenantUpdateDto } from '@src/app/_dtos/tenant-update.dto';
import { TenantDetailsFormSource } from '@src/app/_models/tenant-details-form.model';

import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { GenderEnum } from '@src/app/_enum/gender.enum';

@Component({
  selector: 'ns-tenant-edit-details',
  templateUrl: './tenant-edit-details.component.html',
  styleUrls: ['./tenant-edit-details.component.css'],
})
export class TenantEditDetailsComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  tenantDetailsForm: RadDataFormComponent;

  private _tenantFormSource: TenantDetailsFormSource;
  private _tenantFormSourceOriginal: TenantDetailsFormSource;
  private _isBusy = false;

  private _currentTenant: Tenant;
  constructor(
    private pageRoute: PageRoute,
    private dialogService: DialogService,
    private router: RouterExtensions,
    private translateService: TranslateService,
    private tenantService: TenantService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const tenantId = +paramMap.get('tenantId');
        this.tenantService.getTenant(tenantId).subscribe((tenant: Tenant) => {
          this._currentTenant = tenant;

          this._tenantFormSource = new TenantDetailsFormSource(
            this._currentTenant.firstName,
            this._currentTenant.middleName,
            this._currentTenant.lastName,
            this._currentTenant.gender,
            this._currentTenant.address,
            this._currentTenant.contactNumber,
            this._currentTenant.emailAddress
          );

          this._tenantFormSourceOriginal = this._tenantFormSource.clone();
        });
      });
    });
  }

  saveInformation() {
    const isFormInvalid = this.tenantDetailsForm.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._tenantFormSource.isSame(
      this._tenantFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            const tenantId = this._currentTenant.id;
            const tenantForUpdate = new TenantUpdateDto(
              this._tenantFormSource.firstName,
              this._tenantFormSource.middleName,
              this._tenantFormSource.lastName,
              this._tenantFormSource.gender,
              this._tenantFormSource.address,
              this._tenantFormSource.contactNumber,
              this._tenantFormSource.emailAddress
            );
            this._isBusy = true;

            this.tenantService
              .updateTenant(tenantId, tenantForUpdate)
              .pipe(
                finalize(() => {
                  this._isBusy = false;
                })
              )
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
                    ),
                    () => {
                      this.tenantService.updateTenantListFlag.next();
                      this.router.back();
                    }
                  );
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
                    )
                  );
                }
              );
          }
        });
    }
  }

  get tenantFormSource(): TenantDetailsFormSource {
    return this._tenantFormSource;
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return [
      {
        key: GenderEnum.Male,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.MALE'),
      },
      {
        key: GenderEnum.Female,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.FEMALE'),
      },
    ];
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}
