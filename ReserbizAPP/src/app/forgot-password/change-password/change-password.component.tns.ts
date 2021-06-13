import { Component, NgZone, OnInit } from '@angular/core';
import { RouterExtensions } from '@nativescript/angular';

import { Page } from '@nativescript/core';
import { TranslateService } from '@ngx-translate/core';
import { ButtonOptions } from '@src/app/_enum/button-options.enum';

import { DialogService } from '@src/app/_services/dialog.service';
import { ForgotPasswordService } from '@src/app/_services/forgot-password.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnInit {
  isLoading = false;
  newPassword = '';
  confirmPassword = '';

  constructor(
    private dialogService: DialogService,
    private forgotPasswordService: ForgotPasswordService,
    private ngZone: NgZone,
    private page: Page,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.hideActionBar();
  }

  onSubmit() {
    if (this.isFormValid()) {
      this.dialogService
        .confirm(
          this.translateService.instant(
            'CHANGE_PASSWORD_PAGE.SAVE_NEW_PASSWORD_DIALOG.TITLE'
          ),
          this.translateService.instant(
            'CHANGE_PASSWORD_PAGE.SAVE_NEW_PASSWORD_DIALOG.CONFIRM'
          )
        )
        .then((result: ButtonOptions) => {
          if (result === ButtonOptions.YES) {
            const user = this.forgotPasswordService.user.getValue();

            this.isLoading = true;
            this.forgotPasswordService
              .saveNewPassword(user.id, this.newPassword)
              .subscribe(
                () => {
                  this.isLoading = false;

                  this.dialogService.alert(
                    this.translateService.instant(
                      'CHANGE_PASSWORD_PAGE.SAVE_NEW_PASSWORD_DIALOG.TITLE'
                    ),
                    this.translateService.instant(
                      'CHANGE_PASSWORD_PAGE.SAVE_NEW_PASSWORD_DIALOG.SUCCESS_MESSAGE'
                    ),
                    () => {
                      this.ngZone.run(() => {
                        this.resetForm();
                        this.router.navigate(['auth'], {
                          clearHistory: true,
                        });
                      });
                    }
                  );
                },
                (error: any) => {
                  this.dialogService.alert(
                    this.translateService.instant(
                      'CHANGE_PASSWORD_PAGE.SAVE_NEW_PASSWORD_DIALOG.TITLE'
                    ),
                    error
                  );
                  this.isLoading = false;
                },
                () => {
                  this.resetForm();
                }
              );
          }
        });
    } else {
      this.dialogService.alert(
        this.translateService.instant(
          'CHANGE_PASSWORD_PAGE.SAVE_NEW_PASSWORD_DIALOG.TITLE'
        ),
        this.translateService.instant(
          'CHANGE_PASSWORD_PAGE.SAVE_NEW_PASSWORD_DIALOG.PASSWORD_NOT_MATCHED'
        ),
        () => {
          this.ngZone.run(() => {
            this.resetForm();
          });
        }
      );
    }
  }

  private resetForm() {
    this.newPassword = this.confirmPassword = '';
  }

  private hideActionBar() {
    this.page.actionBarHidden = true;
  }

  private isFormValid(): boolean {
    return Boolean(
      this.newPassword.trim() &&
        this.confirmPassword.trim() &&
        this.newPassword.trim() === this.confirmPassword.trim()
    );
  }

  get formValid(): boolean {
    return Boolean(this.newPassword.trim() && this.confirmPassword.trim());
  }

  get changePasswordButtonText(): string {
    return `${String.fromCharCode(0xf0c7)} ${this.translateService.instant(
      'CHANGE_PASSWORD_PAGE.CHANGE_PASSWORD_BUTTON_CONTROL.LABEL'
    )}`;
  }
}
