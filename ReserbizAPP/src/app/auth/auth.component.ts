import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

import { Page } from '@nativescript/core';
import { RouterExtensions } from '@nativescript/angular';

import { FormService } from '../_services/form.service';
import { AuthService } from '../_services/auth.service';
import { DialogService } from '../_services/dialog.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnInit {
  form: FormGroup;
  usernameControlIsValid = true;
  passwordControlIsValid = true;
  isLoading = false;

  @ViewChild('usernameEl', { static: false }) usernameEl: ElementRef<any>;
  @ViewChild('passwordEl', { static: false }) passwordEl: ElementRef<any>;

  constructor(
    private authService: AuthService,
    private dialogService: DialogService,
    private formService: FormService,
    private page: Page,
    private router: RouterExtensions,
    private translateService: TranslateService
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
    });

    this.controlStatusListener();
    this.hideActionBar();
  }

  hideActionBar() {
    this.page.actionBarHidden = true;
  }

  onSubmit() {
    this.formService.dismiss([
      this.usernameEl.nativeElement,
      this.passwordEl.nativeElement,
    ]);

    if (!this.form.valid) {
      return;
    }

    const username = this.form.get('username').value;
    const password = this.form.get('password').value;
    this.usernameControlIsValid = true;
    this.passwordControlIsValid = true;
    this.isLoading = true;

    this.authService.login(username, password).subscribe(
      (resData) => {
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
  }

  controlStatusListener() {
    this.form.get('username').statusChanges.subscribe((status) => {
      this.usernameControlIsValid = status === 'VALID';
    });

    this.form.get('password').statusChanges.subscribe((status) => {
      this.passwordControlIsValid = status === 'VALID';
    });
  }

  onDone() {
    this.formService.dismiss([
      this.usernameEl.nativeElement,
      this.passwordEl.nativeElement,
    ]);
  }
}
