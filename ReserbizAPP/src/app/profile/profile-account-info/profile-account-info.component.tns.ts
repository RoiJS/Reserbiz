import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { RouterExtensions } from '@nativescript/angular';

import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';
import { DataFormEventData } from 'nativescript-ui-dataform';

import { AuthService } from '@src/app/_services/auth.service';
import { DialogService } from '@src/app/_services/dialog.service';
import { StorageService } from '@src/app/_services/storage.service';

import { User } from '@src/app/_models/user.model';
import { UserAccountInfoFormSource } from '@src/app/_models/form/user-account-form.model';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

@Component({
  selector: 'ns-profile-account-info',
  templateUrl: './profile-account-info.component.html',
  styleUrls: ['./profile-account-info.component.scss'],
})
export class ProfileAccountInfoComponent implements OnInit, OnDestroy {
  @ViewChild(RadDataFormComponent, { static: false })
  profileForm: RadDataFormComponent;

  private _userAccountFormSource: UserAccountInfoFormSource;
  private _userAccountFormOriginal: UserAccountInfoFormSource;

  private _currentUser: User;
  private _currentUserSub: Subscription;

  private _isBusy = false;

  constructor(
    private authService: AuthService,
    private dialogService: DialogService,
    private router: RouterExtensions,
    private translateService: TranslateService,
    private storageService: StorageService
  ) {}

  ngOnInit() {
    this._currentUserSub = this.authService.user.subscribe(
      (currentUser: User) => {
        this._currentUser = currentUser;

        this._userAccountFormSource = new UserAccountInfoFormSource(
          currentUser.username,
          currentUser.emailAddress,
          '',
          ''
        );

        this._userAccountFormOriginal = this._userAccountFormSource.clone();
      }
    );
  }

  ngOnDestroy() {
    if (this._currentUserSub) {
      this._currentUserSub.unsubscribe();
    }
  }

  saveAccountInformation() {
    this.profileForm.dataForm
      .validateAndCommitAll()
      .then((isFormValid: boolean) => {
        const isFormHasChanged = !this._userAccountFormSource.isSame(
          this._userAccountFormOriginal
        );

        if (isFormValid && isFormHasChanged) {
          this.dialogService
            .confirm(
              this.translateService.instant(
                'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
              ),
              this.translateService.instant(
                'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.CONFIRM_MESSAGE'
              )
            )
            .then((res) => {
              if (res === ButtonOptions.YES) {
                this._isBusy = true;

                this.authService
                  .updateAccountInformation(this._userAccountFormSource)
                  .pipe(
                    finalize(() => {
                      this._isBusy = false;
                    })
                  )
                  .subscribe(
                    () => {
                      this.dialogService.alert(
                        this.translateService.instant(
                          'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
                        ),
                        this.translateService.instant(
                          'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.SUCCESS_MESSAGE'
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
                          'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.TITLE'
                        ),
                        `${this.translateService.instant(
                          'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.UPDATE_DIALOG.ERROR_MESSAGE'
                        )} ${error.message}`
                      );
                    }
                  );
              }
            });
        }
      });
  }

  refreshUser(): void {
    const currentUser = new User(
      this._currentUser.firstName,
      this._currentUser.middleName,
      this._currentUser.lastName,
      this._userAccountFormSource.username,
      this._currentUser.gender,
      this._userAccountFormSource.emailAddress
    );

    this.authService.user.next(currentUser);
    this.authService.currentUsername.next(currentUser.username);

    this.storageService.storeString('userData', JSON.stringify(currentUser));
  }

  onPropertyValidate(args: DataFormEventData) {
    if (args.propertyName === 'username') {
      this.onUsernameValidate(args);
    }

    if (args.propertyName === 'emailAddress') {
      this.onEmailAddressValidate(args);
    }

    if (args.propertyName === 'confirmPassword') {
      this.onConfirmPasswordValidate(args);
    }
  }

  onUsernameValidate(args: DataFormEventData) {
    // Check if username is empty or not.
    if (!args.entityProperty.valueCandidate) {
      args.returnValue = false;
      args.entityProperty.errorMessage = this.translateService.instant(
        'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.USERNAME_CONTROL.USERNAME_EMPTY_ERROR_MESSAGE'
      );
      return;
    }

    // Check if username is already exists.
    args.returnValue = new Promise<Boolean>((resolve) => {
      this.authService
        .validateUsernameExists(args.entityProperty.valueCandidate)
        .subscribe((res: boolean) => {
          if (res) {
            args.entityProperty.errorMessage = this.translateService.instant(
              'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.USERNAME_CONTROL.USERNAME_EXISTS_ERROR_MESSAGE'
            );
          }
          resolve(!res);
        });
    });
  }

  onEmailAddressValidate(args: DataFormEventData) {
    // Check if email address is empty or not.
    if (!args.entityProperty.valueCandidate) {
      args.returnValue = false;
      args.entityProperty.errorMessage = this.translateService.instant(
        'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.EMAILADDRESS_CONTROL.EMAILADDRESS_EMPTY_ERROR_MESSAGE'
      );
      return;
    }

    // Check if email address is in invalid format
    if (!this.validateEmail(args.entityProperty.valueCandidate)) {
      args.returnValue = false;
      args.entityProperty.errorMessage = this.translateService.instant(
        'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.EMAILADDRESS_CONTROL.EMAILADDRESS_INVALID_ERROR_MESSAGE'
      );
      return;
    }
  }

  onConfirmPasswordValidate(args: DataFormEventData) {
    const dataForm = args.object;
    const password = dataForm.getPropertyByName('password');
    const confirmPassword = args.entityProperty;

    if (!password.valueCandidate && !confirmPassword.valueCandidate) {
      args.returnValue = true;
      return;
    }

    // Check if password and confirm password fields have the same value.
    if (password.valueCandidate !== confirmPassword.valueCandidate) {
      confirmPassword.errorMessage = this.translateService.instant(
        'ACCOUNT_INFORMATION_PAGE.FORM_CONTROL.CONFIRM_PASSWORD_CONTROL.CONFIRM_PASSWORD_DOES_NOT_MATCH_ERROR_MESSAGE'
      );
      args.returnValue = false;
      return;
    }

    args.returnValue = true;
  }

  private validateEmail(email: string): boolean {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
  }

  get userAccountFormSource(): UserAccountInfoFormSource {
    return this._userAccountFormSource;
  }

  get IsBusy(): boolean {
    return this._isBusy;
  }
}
