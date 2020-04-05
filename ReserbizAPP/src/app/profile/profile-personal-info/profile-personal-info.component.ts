import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { TranslateService } from '@ngx-translate/core';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { AuthService } from '@src/app/_services/auth.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { TextService } from '@src/app/_services/text.service.tns';

import { GenderEnum } from '@src/app/_enum/gender.enum';
import { User } from '@src/app/_models/user.model';
import { UserPersonalInfoFormSource } from '@src/app/_models/user-personal-form.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'ns-profile',
  templateUrl: './profile-personal-info.component.html',
  styleUrls: ['./profile-personal-info.component.scss']
})
export class ProfilePersonalInfoComponent implements OnInit, OnDestroy {
  @ViewChild(RadDataFormComponent, { static: false })
  profileForm: RadDataFormComponent;

  private _userFormSource: UserPersonalInfoFormSource;
  private _userFormSourceOriginal: UserPersonalInfoFormSource;
  private _currentUser: User;
  private _currentUserSub: Subscription;

  private _isBusy = false;

  constructor(
    private authService: AuthService,
    private dialogService: DialogService,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this._currentUserSub = this.authService.user.subscribe(
      (currentUser: User) => {
        this._currentUser = currentUser;

        this._userFormSource = new UserPersonalInfoFormSource(
          this._currentUser.firstName,
          this._currentUser.middleName,
          this._currentUser.lastName,
          this._currentUser.gender
        );

        this._userFormSourceOriginal = this._userFormSource.clone();
      }
    );
  }

  ngOnDestroy(): void {
    if (this._currentUserSub) {
      this._currentUserSub.unsubscribe();
    }
  }

  savePersonalInformation() {
    const isFormInvalid = this.profileForm.dataForm.hasValidationErrors();
    const isFormHasChanged = !this._userFormSource.isSame(
      this._userFormSourceOriginal
    );

    if (!isFormInvalid && isFormHasChanged) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'PERSONAL_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'PERSONAL_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
          )
        )
        .then(res => {
          if (res === ButtonOptions.YES) {
            this._isBusy = true;

            this.authService
              .updatePersonalInformation(this._userFormSource)
              .pipe(
                finalize(() => {
                  this._isBusy = false;
                })
              )
              .subscribe(
                () => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'PERSONAL_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'PERSONAL_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
                    )
                  );

                  this._userFormSourceOriginal = this._userFormSource.clone();

                  this.refreshUser();
                },
                (error: Error) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'PERSONAL_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
                    ),
                    `${this.translateService.instant(
                      'PERSONAL_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
                    )} ${error.message}`
                  );
                }
              );
          }
        });
    }
  }

  refreshUser(): void {
    const currentUser = new User(
      this._userFormSource.firstName,
      this._userFormSource.middleName,
      this._userFormSource.lastName,
      this._currentUser.username,
      this._userFormSource.gender
    );

    this.authService.user.next(currentUser);
    this.authService.currentFullname.next(currentUser.fullname);
  }

  get userFormSource(): UserPersonalInfoFormSource {
    return this._userFormSource;
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return [
      {
        key: GenderEnum.Male,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.MALE')
      },
      {
        key: GenderEnum.Female,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.FEMALE')
      }
    ];
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}
