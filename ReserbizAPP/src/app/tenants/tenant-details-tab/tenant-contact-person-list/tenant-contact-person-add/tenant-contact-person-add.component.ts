import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { finalize } from 'rxjs/operators';

import { ContactPersonDetailsFormSource } from '@src/app/_models/contact-person-details-form.model';
import { ContactPersonCreateDto } from '@src/app/_dtos/contact-person-create.dto';
import { DialogService } from '@src/app/_services/dialog.service';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { ContactPersonService } from '@src/app/_services/contact-person.service';
import { GenderEnum } from '@src/app/_enum/gender.enum';

@Component({
  selector: 'ns-tenant-contact-person-add',
  templateUrl: './tenant-contact-person-add.component.html',
  styleUrls: ['./tenant-contact-person-add.component.scss'],
})
export class TenantContactPersonAddComponent implements OnInit {
  @ViewChild(RadDataFormComponent, { static: false })
  contactPersonForm: RadDataFormComponent;

  private _contactPersonFormSource: ContactPersonDetailsFormSource;
  private _isBusy = false;
  private _currentTenantId: number;

  constructor(
    private contactPesonService: ContactPersonService,
    private dialogService: DialogService,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this._currentTenantId = +paramMap.get('tenantId');
        this._contactPersonFormSource = new ContactPersonDetailsFormSource(
          '',
          '',
          '',
          GenderEnum.Male,
          ''
        );
      });
    });
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
            const contactPersonForCreate = new ContactPersonCreateDto(
              this._contactPersonFormSource.firstName,
              this._contactPersonFormSource.middleName,
              this._contactPersonFormSource.lastName,
              this._contactPersonFormSource.gender,
              this._contactPersonFormSource.contactNumber
            );
            this._isBusy = true;

            this.contactPesonService
              .createContactPerson(
                this._currentTenantId,
                contactPersonForCreate
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
                      'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.SUCCESS_MESSAGE'
                    ),
                    () => {
                      this.router.back();
                    }
                  );
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.UPDATE_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'TENANT_CONTACT_PERSON_ADD_DETAILS_PAGE.FORM_CONTROL.ADD_DIALOG.ERROR_MESSAGE'
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
