import { Component, OnInit, ViewChild } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

import { RouterExtensions } from "@nativescript/angular";
import { RadDataFormComponent } from "nativescript-ui-dataform/angular";

import { DialogService } from "~/app/_services/dialog.service";
import { AddContactPersonsService } from "~/app/_services/add-contact-persons.service";

import { GenderEnum } from "~/app/_enum/gender.enum";

import { ContactPerson } from "~/app/_models/contact-person.model";
import { ContactPersonDetailsFormSource } from "~/app/_models/form/contact-person-details-form.model";

import { ContactPersonMapper } from "~/app/_helpers/mappers/contact-person-mapper.helper";
import { GenderValueProvider } from "~/app/_helpers/value_providers/gender-value-provider.helper";

import { IGenderValueProvider } from "~/app/_interfaces/value_providers/igender-value-provider.interface";

@Component({
  selector: "ns-tenant-add-contact-person-add",
  templateUrl: "./tenant-add-contact-person-add.component.html",
  styleUrls: ["./tenant-add-contact-person-add.component.scss"],
})
export class TenantAddContactPersonAddComponent
  implements IGenderValueProvider, OnInit
{
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
            "TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
          ),
          this.translateService.instant(
            "TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.CONFIRM_MESSAGE"
          )
        )
        .then((res: boolean) => {
          if (res) {
            this._isBusy = true;

            setTimeout(() => {
              const newContactPerson = new ContactPerson();

              newContactPerson.firstName =
                this._contactPersonFormSource.firstName;
              newContactPerson.middleName =
                this._contactPersonFormSource.middleName;
              newContactPerson.lastName =
                this._contactPersonFormSource.lastName;
              newContactPerson.gender = this._contactPersonFormSource.gender;
              newContactPerson.contactNumber =
                this._contactPersonFormSource.contactNumber;
              newContactPerson.relation =
                this._contactPersonFormSource.relation;

              this.addContactPersonsService.addNewEntity(newContactPerson);

              this.dialogService
                .alert(
                  this.translateService.instant(
                    "TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE"
                  ),
                  this.translateService.instant(
                    "TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE"
                  )
                )
                .then(() => {
                  this.router.back();
                });
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
