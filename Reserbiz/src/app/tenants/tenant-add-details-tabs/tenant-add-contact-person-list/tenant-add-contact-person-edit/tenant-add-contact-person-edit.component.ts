import { Component, OnInit, ViewChild } from "@angular/core";
import { PageRoute, RouterExtensions } from "@nativescript/angular";
import { TranslateService } from "@ngx-translate/core";

import { RadDataFormComponent } from "nativescript-ui-dataform/angular";

import { DialogService } from "~/app/_services/dialog.service";
import { AddContactPersonsService } from "~/app/_services/add-contact-persons.service";

import { ContactPersonDetailsFormSource } from "~/app/_models/form/contact-person-details-form.model";
import { ContactPerson } from "~/app/_models/contact-person.model";

import { GenderEnum } from "~/app/_enum/gender.enum";

import { IGenderValueProvider } from "~/app/_interfaces/value_providers/igender-value-provider.interface";

import { GenderValueProvider } from "~/app/_helpers/value_providers/gender-value-provider.helper";

@Component({
  selector: "ns-tenant-add-contact-person-edit",
  templateUrl: "./tenant-add-contact-person-edit.component.html",
  styleUrls: ["./tenant-add-contact-person-edit.component.scss"],
})
export class TenantAddContactPersonEditComponent
  implements IGenderValueProvider, OnInit
{
  @ViewChild(RadDataFormComponent, { static: false })
  contactPersonForm: RadDataFormComponent;

  private _isBusy = false;
  private _currentContactPersonId: number;
  private _currentContacPerson: ContactPerson;
  private _contactPersonFormSource: ContactPersonDetailsFormSource;
  private _contactPersonFormSourceOriginal: ContactPersonDetailsFormSource;
  private _genderValueProvider: GenderValueProvider;

  constructor(
    private addContactPersonsService: AddContactPersonsService,
    private dialogService: DialogService,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {
    this._genderValueProvider = new GenderValueProvider(this.translateService);
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentContactPersonId = +paramMap.get("contactPersonId");

        this._currentContacPerson = this.addContactPersonsService.getEntity(
          this._currentContactPersonId
        );

        this._contactPersonFormSource = new ContactPersonDetailsFormSource(
          this._currentContacPerson.firstName,
          this._currentContacPerson.middleName,
          this._currentContacPerson.lastName,
          this._currentContacPerson.gender,
          this._currentContacPerson.contactNumber,
          this._currentContacPerson.relation
        );

        this._contactPersonFormSourceOriginal =
          this._contactPersonFormSource.clone();
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
            "TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE"
          ),
          this.translateService.instant(
            "TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE"
          )
        )
        .then((res: boolean) => {
          if (res) {
            this._isBusy = true;

            this._currentContacPerson.firstName =
              this._contactPersonFormSource.firstName;
            this._currentContacPerson.middleName =
              this._contactPersonFormSource.middleName;
            this._currentContacPerson.lastName =
              this._contactPersonFormSource.lastName;
            this._currentContacPerson.gender =
              this._contactPersonFormSource.gender;
            this._currentContacPerson.contactNumber =
              this._contactPersonFormSource.contactNumber;
            this._currentContacPerson.relation =
              this._contactPersonFormSource.relation;

            this.addContactPersonsService.updateEntity(
              this._currentContacPerson
            );

            this.dialogService
              .alert(
                this.translateService.instant(
                  "TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE"
                ),
                this.translateService.instant(
                  "TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE"
                )
              )
              .then(() => {
                this.router.back();
              });
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
