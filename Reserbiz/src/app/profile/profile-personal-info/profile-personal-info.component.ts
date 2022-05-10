import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { TranslateService } from '@ngx-translate/core';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';

import { AuthService } from '../../_services/auth.service';
import { DialogService } from '../../_services/dialog.service';

import { GenderEnum } from '../../_enum/gender.enum';
import { User } from '../../_models/user.model';
import { UserPersonalInfoFormSource } from '../../_models/form/user-personal-form.model';
import { ButtonOptions } from '../../_enum/button-options.enum';
import { finalize } from 'rxjs/operators';
import { GenderValueProvider } from '../../_helpers/value_providers/gender-value-provider.helper';
import { IGenderValueProvider } from '../../_interfaces/value_providers/igender-value-provider.interface';
import { RouterExtensions } from '@nativescript/angular';

@Component({
  selector: 'ns-profile',
  templateUrl: './profile-personal-info.component.html',
  styleUrls: ['./profile-personal-info.component.scss'],
})
export class ProfilePersonalInfoComponent
  implements IGenderValueProvider, OnInit, OnDestroy {
  @ViewChild(RadDataFormComponent, { static: false })
  profileForm: RadDataFormComponent;

  private _userFormSource: UserPersonalInfoFormSource;
  private _userFormSourceOriginal: UserPersonalInfoFormSource;
  private _currentUser: User;
  private _currentUserSub: Subscription;
  private _genderValueProvider: GenderValueProvider;

  private _isBusy = false;

  constructor(
    private authService: AuthService,
    private dialogService: DialogService,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {
    this._genderValueProvider = new GenderValueProvider(this.translateService);
  }

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
        .subscribe((res: ButtonOptions) => {
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
                    ),
                    () => {
                      this.refreshUser();
                      this.router.back();
                    }
                  );
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
      this._userFormSource.gender,
      this._currentUser.emailAddress
    );

    this.authService.user.next(currentUser);
    this.authService.currentFullname.next(currentUser.fullname);
  }

  get userFormSource(): UserPersonalInfoFormSource {
    return this._userFormSource;
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return this._genderValueProvider.genderOptions;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}
