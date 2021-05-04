import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { Page } from '@nativescript/core';
import { getConnectionType } from '@nativescript/core/connectivity';

import { RouterExtensions } from '@nativescript/angular';

import { Client } from '../_models/client.model';

import { AppVersionService } from '../_services/app-version.service';
import { FormService } from '../_services/form.service';
import { AuthService } from '../_services/auth.service';
import { CheckConnectionService } from '../_services/check-connection.service';
import { DialogService } from '../_services/dialog.service';
import { PushNotificationService } from '../_services/push-notification.service';
import { StorageService } from '../_services/storage.service';
import { SettingsService } from '../_services/settings.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnInit {
  isLoading = false;

  @ViewChild('usernameEl', { static: false }) usernameEl: ElementRef<any>;
  @ViewChild('passwordEl', { static: false }) passwordEl: ElementRef<any>;
  @ViewChild('companyEl', { static: false }) companyEl: ElementRef<any>;

  company = '';
  username = '';
  password = '';

  constructor(
    private authService: AuthService,
    private appVersionService: AppVersionService,
    private checkConnectionService: CheckConnectionService,
    private dialogService: DialogService,
    private formService: FormService,
    private page: Page,
    private pushNotificationService: PushNotificationService,
    private router: RouterExtensions,
    private translateService: TranslateService,
    private settingsService: SettingsService,
    private storageService: StorageService
  ) {}

  ngOnInit() {
    this.initializeCompanyField();
    this.hideActionBar();
    this.setCurrentConnection();
  }

  hideActionBar() {
    this.page.actionBarHidden = true;
  }

  onSubmit() {
    if (this.isFormValid()) {
      this.isLoading = true;
      this.authService.checkCompany(this.company).subscribe(
        (client: Client) => {
          this.storageService.storeString('company', client.name);
          this.storageService.storeString(
            'app-secret-token',
            client.dbHashName
          );

          this.authService.login(this.username, this.password).subscribe(
            () => {
              (async () => {
                // Subscribe to push notifications
                this.pushNotificationService.subscribe();

                const notificationNavigateToUrl = this.pushNotificationService.navigateToUrl.getValue();

                await this.settingsService.getSettingsDetails();

                if (!notificationNavigateToUrl) {
                  this.router.navigate(['/dashboard'], {
                    transition: {
                      name: 'slideLeft',
                    },
                    clearHistory: true,
                  });
                }
              })();
            },
            (error: any) => {
              this.dialogService.alert(
                this.translateService.instant('AUTH_PAGE.LOGIN_FAILED'),
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
            this.translateService.instant('AUTH_PAGE.LOGIN_FAILED'),
            error
          );
          this.isLoading = false;
          this.resetForm();
        }
      );
    }
  }

  onDone() {
    this.formService.dismiss([
      this.usernameEl.nativeElement,
      this.passwordEl.nativeElement,
    ]);
  }

  private initializeCompanyField() {
    let company = '';

    if (this.storageService.hasKey('company')) {
      company = this.storageService.getString('company');
    }

    this.company = company;
  }

  private setCurrentConnection() {
    const currentConnectionType = getConnectionType();
    this.checkConnectionService.currentConnectionType.next(
      currentConnectionType
    );
  }

  private isFormValid(): boolean {
    return Boolean(
      this.company.trim() && this.username.trim() && this.password.trim()
    );
  }

  private resetForm() {
    this.company = this.username = this.password = '';
  }

  get copyRightText(): string {
    return this.appVersionService.copyRightText;
  }

  get appVersion(): string {
    return this.appVersionService.appVersion;
  }

  get formValid(): boolean {
    return this.isFormValid();
  }

  get loginButtonText(): string {
    return `${String.fromCharCode(0xf2f6)} ${this.translateService.instant(
      'AUTH_PAGE.LOGIN_BUTTON_CONTROL.LABEL'
    )}`;
  }
}
