import { Component, OnInit, NgZone } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';

import { TenantService } from '@src/app/_services/tenant.service';
import { DialogService } from '@src/app/_services/dialog.service';

import { Tenant } from '@src/app/_models/tenant.model';
import { TenantDetailsFormSource } from '@src/app/_models/tenant-details-form.model';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';
import { GenderEnum } from '@src/app/_enum/gender.enum';
import { TenantDto } from '@src/app/_dtos/tenant-create.dto';
import { IBaseFormComponent } from '@src/app/_interfaces/ibase-form.component.interface';
import { TenantMapper } from '@src/app/_helpers/tenant-mapper.helper';

@Component({
  selector: 'ns-tenant-edit-details',
  templateUrl: './tenant-edit-details.component.html',
  styleUrls: ['./tenant-edit-details.component.css'],
})
export class TenantEditDetailsComponent
  extends BaseFormComponent<Tenant, TenantDetailsFormSource, TenantDto>
  implements IBaseFormComponent, OnInit {
  constructor(
    public pageRoute: PageRoute,
    public dialogService: DialogService,
    public ngZone: NgZone,
    public router: RouterExtensions,
    public translateService: TranslateService,
    public tenantService: TenantService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = tenantService;
    this._entityDtoMapper = new TenantMapper();
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentFormEntityId = +paramMap.get('tenantId');
        this.tenantService
          .getTenant(this._currentFormEntityId)
          .subscribe((tenant: Tenant) => {
            this._currentEntity = tenant;

            this._entityFormSource = this._entityDtoMapper.mapEntityToFormSource(
              tenant
            );

            this._entityFormSourceOriginal = this._entityFormSource.clone();
          });
      });
    });

    this.initDialogTexts();
  }

  initDialogTexts() {
    this._updateDialogTexts = {
      title: this.translateService.instant(
        'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANT_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
      ),
    };
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
}
