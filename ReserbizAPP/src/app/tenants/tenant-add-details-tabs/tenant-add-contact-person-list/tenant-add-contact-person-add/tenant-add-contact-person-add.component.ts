import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { DialogService } from '@src/app/_services/dialog.service';
import { AddContactPersonsService } from '@src/app/_services/add-contact-persons.service';
import { GenderEnum } from '@src/app/_enum/gender.enum';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { ContactPersonDetailsFormSource } from '@src/app/_models/contact-person-details-form.model';
import { ContactPersonMapper } from '@src/app/_helpers/contact-person-mapper.helper';
import { IGenderValueProvider } from '@src/app/_interfaces/igender-value-provider.interface';
import { GenderValueProvider } from '@src/app/_helpers/gender-value-provider.helper';

@Component({
  selector: 'ns-tenant-add-contact-person-add',
  templateUrl: './tenant-add-contact-person-add.component.html',
  styleUrls: ['./tenant-add-contact-person-add.component.css'],
})
export class TenantAddContactPersonAddComponent
  implements IGenderValueProvider, OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  contactPersonForm: RadDataFormComponent;

  private _contactPersonFormSource: ContactPersonDetailsFormSource;
  private _isBusy = false;
  private _genderValueProvider: GenderValueProvider;

  constructor(
    private addContactPersonsService: AddContactPersonsService,
    private dialogService: DialogService,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {
    this._genderValueProvider = new GenderValueProvider(this.translateService);
  }

  ngOnInit() {
    this._contactPersonFormSource = new ContactPersonMapper().initFormSource();
  }

  saveInformation() {
    const isFormInvalid = this.contactPersonForm.dataForm.hasValidationErrors();

    if (!isFormInvalid) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            setTimeout(() => {
              const newContactPerson = new ContactPerson();

              newContactPerson.firstName = this._contactPersonFormSource.firstName;
              newContactPerson.middleName = this._contactPersonFormSource.middleName;
              newContactPerson.lastName = this._contactPersonFormSource.lastName;
              newContactPerson.gender = this._contactPersonFormSource.gender;
              newContactPerson.contactNumber = this._contactPersonFormSource.contactNumber;

              this.addContactPersonsService.addNewEntity(newContactPerson);

              this.dialogService.alert(
                this.translateService.instant(
                  'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
                ),
                this.translateService.instant(
                  'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
                ),
                () => {
                  this.router.back();
                }
              );
            }, 1000);
          }
        });
    }
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return this._genderValueProvider.genderOptions;
  }

  get contactPersonFormSource(): ContactPersonDetailsFormSource {
    return this._contactPersonFormSource;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}
