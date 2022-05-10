import { Component, OnInit, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageRoute, RouterExtensions } from '@nativescript/angular';

import { BaseFormComponent } from '../../../../shared/component/base-form.component';

import { ContactPersonDetailsFormSource } from '../../../../_models/form/contact-person-details-form.model';
import { ContactPerson } from '../../../../_models/contact-person.model';
import { ContactPersonDto } from '../../../../_dtos/contact-person.dto';
import { GenderEnum } from '../../../../_enum/gender.enum';

import { IBaseFormComponent } from '../../../../_interfaces/components/ibase-form.component.interface';
import { ContactPersonMapper } from '../../../../_helpers/mappers/contact-person-mapper.helper';

import { DialogService } from '../../../../_services/dialog.service';
import { ContactPersonService } from '../../../../_services/contact-person.service';
import { IGenderValueProvider } from '../../../../_interfaces/value_providers/igender-value-provider.interface';
import { GenderValueProvider } from '../../../../_helpers/value_providers/gender-value-provider.helper';

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
