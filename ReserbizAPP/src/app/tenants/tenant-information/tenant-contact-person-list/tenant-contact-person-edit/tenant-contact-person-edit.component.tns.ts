import { Component, OnInit, NgZone } from '@angular/core';
import { PageRoute, RouterExtensions } from '@nativescript/angular';

import { TranslateService } from '@ngx-translate/core';

import { take, finalize } from 'rxjs/operators';

import { ContactPersonDetailsFormSource } from '@src/app/_models/form/contact-person-details-form.model';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { GenderEnum } from '@src/app/_enum/gender.enum';
import { ContactPersonDto } from '@src/app/_dtos/contact-person.dto';
import { ContactPersonService } from '@src/app/_services/contact-person.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { BaseFormComponent } from '@src/app/shared/component/base-form.component';
import { IBaseFormComponent } from '@src/app/_interfaces/components/ibase-form.component.interface';
import { ContactPersonMapper } from '@src/app/_helpers/mappers/contact-person-mapper.helper';
import { IGenderValueProvider } from '@src/app/_interfaces/value_providers/igender-value-provider.interface';
import { GenderValueProvider } from '@src/app/_helpers/value_providers/gender-value-provider.helper';

@Component({
  selector: 'ns-tenant-contact-person-edit',
  templateUrl: './tenant-contact-person-edit.component.html',
  styleUrls: ['./tenant-contact-person-edit.component.scss'],
})
export class TenantContactPersonEditComponent
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
        this._currentFormEntityId = +paramMap.get('contactPersonId');

        this.contactPersonService
          .getContactPerson(this._currentFormEntityId)
          .pipe(
            take(1),
            finalize(() => (this._isBusy = false))
          )
          .subscribe((contactPerson: ContactPerson) => {
            this._entityFormSource = this._entityDtoMapper.mapEntityToFormSource(
              contactPerson
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
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
      ),
      confirmMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
      ),
      successMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
      ),
      errorMessage: this.translateService.instant(
        'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
      ),
    };
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return this._genderValueProvider.genderOptions;
  }
}
