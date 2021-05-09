import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Page } from '@nativescript/core';

import { RouterExtensions } from '@nativescript/angular';

import { TranslateService } from '@ngx-translate/core';

import { AuthService } from '../_services/auth.service';
import { DialogService } from '../_services/dialog.service';
import { ForgotPasswordService } from '../_services/forgot-password.service';

import { Client } from '../_models/client.model';

@Component({
  selector: 'ns-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent implements OnInit {
  isLoading = false;
  company = '';
  usernameOrEmailAddress = '';
  constructor(
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private dialogService: DialogService,
    private forgotPasswordService: ForgotPasswordService,
    private page: Page,
    private router: RouterExtensions,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.hideActionBar();
  }

  onSubmit() {
    if (this.isFormValid()) {
      this.isLoading = true;
      this.authService.checkCompany(this.company).subscribe(
        (client: Client) => {
          this.forgotPasswordService.appSecretToken.next(client.dbHashName);

          this.forgotPasswordService
            .verifyUsernameOrEmailAddress(this.usernameOrEmailAddress)
            .subscribe(
              () => {
                this.isLoading = false;
                this.router.navigate(['change-password'], {
                  relativeTo: this.activatedRoute,
                  clearHistory: true,
                });
              },
              (error: any) => {
                this.dialogService.alert(
                  this.translateService.instant(
                    'FORGOT_PASSWORD_PAGE.VERIFICATION_FAILED'
                  ),
                  error
                );
                this.isLoading = false;
              },
              () => {
                this.resetForm();
              }
            );
        },
        (error: any) => {
          this.dialogService.alert(
            this.translateService.instant(
              'FORGOT_PASSWORD_PAGE.VERIFICATION_FAILED'
            ),
            error
          );
          this.isLoading = false;
          this.resetForm();
        }
      );
    }
  }

  private resetForm() {
    this.company = this.usernameOrEmailAddress = '';
  }

  private hideActionBar() {
    this.page.actionBarHidden = true;
  }

  private isFormValid(): boolean {
    return Boolean(this.company.trim() && this.usernameOrEmailAddress.trim());
  }

  get formValid(): boolean {
    return this.isFormValid();
  }

  get verifyButtonText(): string {
    return `${String.fromCharCode(0xf2f6)} ${this.translateService.instant(
      'FORGOT_PASSWORD_PAGE.VERIFY_BUTTON_CONTROL.LABEL'
    )}`;
  }
}
