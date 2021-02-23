import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

import { Page } from '@nativescript/core';
import { RouterExtensions } from '@nativescript/angular';

import { Client } from '../_models/client.model';

import { FormService } from '../_services/form.service';
import { AuthService } from '../_services/auth.service';
import { DialogService } from '../_services/dialog.service';
import { StorageService } from '../_services/storage.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnInit {
  form: FormGroup;
  isLoading = false;

  @ViewChild('usernameEl', { static: false }) usernameEl: ElementRef<any>;
  @ViewChild('passwordEl', { static: false }) passwordEl: ElementRef<any>;
  @ViewChild('companyEl', { static: false }) companyEl: ElementRef<any>;

  constructor(
    private authService: AuthService,
    private dialogService: DialogService,
    private formService: FormService,
    private page: Page,
    private router: RouterExtensions,
    private translateService: TranslateService,
    private storageService: StorageService
  ) {}

  ngOnInit() {
    this.form = new FormGroup({
      username: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required],
      }),
      password: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required],
      }),
      company: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required],
      }),
    });

    this.initializeCompanyField();
    this.hideActionBar();
  }

  hideActionBar() {
    this.page.actionBarHidden = true;
  }

  onSubmit() {
    this.formService.dismiss([
      this.usernameEl.nativeElement,
      this.passwordEl.nativeElement,
      this.companyEl.nativeElement,
    ]);

    if (!this.form.valid) {
      return;
    }

    const username = this.form.get('username').value;
    const password = this.form.get('password').value;
    const company = this.form.get('company').value;

    this.isLoading = true;

    this.authService.checkCompany(company).subscribe(
      (client: Client) => {
        this.storageService.storeString('company', client.name);
        this.storageService.storeString('app-secret-token', client.dbHashName);

        this.authService.login(username, password).subscribe(
          () => {
            this.router.navigate(['/dashboard'], {
              transition: {
                name: 'slideLeft',
              },
            });
          },
          (error: any) => {
            this.dialogService.alert(
              this.translateService.instant('AUTH_PAGE.LOGIN_FAILED'),
              error
            );
            this.isLoading = false;
          },
          () => {
            this.form.reset();
          }
        );
      },
      (error: any) => {
        this.dialogService.alert(
          this.translateService.instant('AUTH_PAGE.LOGIN_FAILED'),
          error
        );
        this.isLoading = false;
        this.form.reset();
      }
    );
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

    this.form.get('company').setValue(company);
  }
}
