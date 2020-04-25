import { Component, OnInit, ViewChild } from '@angular/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { TranslateService } from '@ngx-translate/core';
import { take, finalize } from 'rxjs/operators';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { ContactPersonDetailsFormSource } from '@src/app/_models/contact-person-details-form.model';
import { ContactPerson } from '@src/app/_models/contact-person.model';
import { ContactPersonService } from '@src/app/_services/contact-person.service';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { ContactPersonUpdateDto } from '@src/app/_dtos/contact-person-update.dto';
import { DialogService } from '@src/app/_services/dialog.service';
import { GenderEnum } from '@src/app/_enum/gender.enum';

@Component({
  selector: 'ns-tenant-contact-person-edit',
  templateUrl: './tenant-contact-person-edit.component.html',
  styleUrls: ['./tenant-contact-person-edit.component.scss'],
})
export class TenantContactPersonEditComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  contactPersonForm: RadDataFormComponent;

  private _contactPersonFormSource: ContactPersonDetailsFormSource;
  private _contactPersonFormSourceOriginal: ContactPersonDetailsFormSource;

  private _isBusy = false;
  private _currentContactPersonId: number;

  constructor(
    private contactPersonService: ContactPersonService,
    private dialogService: DialogService,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentContactPersonId = +paramMap.get('contactPersonId');

        this.contactPersonService
          .getContactPerson(this._currentContactPersonId)
          .pipe(
            take(1),
            finalize(() => (this._isBusy = false))
          )
          .subscribe((contactPerson: ContactPerson) => {
            this._contactPersonFormSource = new ContactPersonDetailsFormSource(
              contactPerson.firstName,
              contactPerson.middleName,
              contactPerson.lastName,
              contactPerson.gender,
              contactPerson.contactNumber
            );

            this._contactPersonFormSourceOriginal = this._contactPersonFormSource.clone();
          });
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
            const contactPersonForUpdate = new ContactPersonUpdateDto(
              this._contactPersonFormSource.firstName,
              this._contactPersonFormSource.middleName,
              this._contactPersonFormSource.lastName,
              this._contactPersonFormSource.gender,
              this._contactPersonFormSource.contactNumber
            );
            this._isBusy = true;

            this.contactPersonService
              .updateContactPerson(
                this._currentContactPersonId,
                contactPersonForUpdate
              )
              .pipe(
                finalize(() => {
                  this._isBusy = false;
                })
              )
              .subscribe(
                () => {
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
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_EDIT_DETAILS_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
                    )
                  );
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
