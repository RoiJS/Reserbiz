import { Component, OnInit, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageRoute, RouterExtensions } from 'nativescript-angular/router';

import { BaseFormComponent } from '@src/app/shared/component/base-form.component';

import { ContactPersonDetailsFormSource } from '@src/app/_models/contact-person-details-form.model';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { ContactPersonDto } from '@src/app/_dtos/contact-person.dto';
import { GenderEnum } from '@src/app/_enum/gender.enum';

import { IBaseFormComponent } from '@src/app/_interfaces/ibase-form.component.interface';
import { ContactPersonMapper } from '@src/app/_helpers/contact-person-mapper.helper';

import { DialogService } from '@src/app/_services/dialog.service';
import { ContactPersonService } from '@src/app/_services/contact-person.service';
import { IGenderValueProvider } from '@src/app/_interfaces/igender-value-provider.interface';
import { GenderValueProvider } from '@src/app/_helpers/gender-value-provider.helper';

@Component({
  selector: 'ns-tenant-contact-person-add',
  templateUrl: './tenant-contact-person-add.component.html',
  styleUrls: ['./tenant-contact-person-add.component.scss'],
})
export class TenantContactPersonAddComponent
  extends BaseFormComponent<
    ContactPerson,
    ContactPersonDetailsFormSource,
    ContactPersonDto
  >
  implements IBaseFormComponent, IGenderValueProvider, OnInit {
  private _genderValueProvider: GenderValueProvider;
  constructor(
    public contactPersonService: ContactPersonService,
    public dialogService: DialogService,
    public ngZone: NgZone,
    public pageRoute: PageRoute,
    public router: RouterExtensions,
    public translateService: TranslateService
  ) {
    super(dialogService, ngZone, router, translateService);
    this._entityService = contactPersonService;
    this._entityDtoMapper = new ContactPersonMapper();
    this._genderValueProvider = new GenderValueProvider(this.translateService);
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentFormEntityId = +paramMap.get('tenantId');
        this._entityFormSource = this._entityDtoMapper.initFormSource();
      });
    });

    this.initDialogTexts();
  }

  initDialogTexts() {
    this._saveNewDialogTexts = {
      title: this.translateService.instant(
        'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return this._genderValueProvider.genderOptions;
  }
}
