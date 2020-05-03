import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { TranslateService } from '@ngx-translate/core';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { Subscription } from 'rxjs';

import { DialogService } from '@src/app/_services/dialog.service';
import { AddContactPersonsService } from '@src/app/_services/add-contact-persons.service';
import { ContactPersonDetailsFormSource } from '@src/app/_models/contact-person-details-form.model';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { GenderEnum } from '@src/app/_enum/gender.enum';

@Component({
  selector: 'ns-tenant-add-contact-person-edit',
  templateUrl: './tenant-add-contact-person-edit.component.html',
  styleUrls: ['./tenant-add-contact-person-edit.component.css'],
})
export class TenantAddContactPersonEditComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  contactPersonForm: RadDataFormComponent;

  private _isBusy = false;
  private _currentContactPersonId: number;
  private _currentContacPerson: ContactPerson;
  private _contactPersonFormSource: ContactPersonDetailsFormSource;
  private _contactPersonFormSourceOriginal: ContactPersonDetailsFormSource;

  constructor(
    private addContactPersonsService: AddContactPersonsService,
    private dialogService: DialogService,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentContactPersonId = +paramMap.get('contactPersonId');

        this._currentContacPerson = this.addContactPersonsService.getContactPerson(
          this._currentContactPersonId
        );

        this._contactPersonFormSource = new ContactPersonDetailsFormSource(
          this._currentContacPerson.firstName,
          this._currentContacPerson.middleName,
          this._currentContacPerson.lastName,
          this._currentContacPerson.gender,
          this._currentContacPerson.contactNumber
        );

        this._contactPersonFormSourceOriginal = this._contactPersonFormSource.clone();
      });
    });
  }

  saveInformation() {
    const isFormInvalid = this.contactPersonForm.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._contactPersonFormSource.isSame(
      this._contactPersonFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then((res) => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this._currentContacPerson.firstName = this._contactPersonFormSource.firstName;
            this._currentContacPerson.middleName = this._contactPersonFormSource.middleName;
            this._currentContacPerson.lastName = this._contactPersonFormSource.lastName;
            this._currentContacPerson.gender = this._contactPersonFormSource.gender;
            this._currentContacPerson.contactNumber = this._contactPersonFormSource.contactNumber;

            this.addContactPersonsService.updateContactPerson(
              this._currentContacPerson
            );

            this.dialogService.alert(
              this.translateService.instant(
                'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
              ),
              this.translateService.instant(
                'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
              ),
              () => {
                this.router.back();
              }
            );
          }
        });
    }
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

  get contactPersonFormSource(): ContactPersonDetailsFormSource {
    return this._contactPersonFormSource;
  }

  get isBusy(): boolean {
    return this._isBusy;
  }
}
