import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { Page } from 'tns-core-modules/ui/page/page';

import { FormService } from '../_services/form.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  form: FormGroup;
  usernameControlIsValid = true;
  passwordControlIsValid = true;
  isLoading = false;

  @ViewChild('usernameEl', { static: false }) usernameEl: ElementRef<any>;
  @ViewChild('passwordEl', { static: false }) passwordEl: ElementRef<any>;

  constructor(
    private router: Router,
    private authService: AuthService,
    private formService: FormService,
    private page: Page,
    private translate: TranslateService
  ) {}

  ngOnInit() {
    this.form = new FormGroup({
      username: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required]
      }),
      password: new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required, Validators.minLength(6)]
      })
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
      this.passwordEl.nativeElement
    ]);

    if (!this.form.valid) {
      return;
    }

    const username = this.form.get('username').value;
    const password = this.form.get('password').value;
    this.form.reset();
    this.usernameControlIsValid = true;
    this.passwordControlIsValid = true;
    this.isLoading = true;
    this.authService.login(username, password).subscribe(
      resData => {
        this.isLoading = false;
        this.router.navigate(['/dashboard']);
      },
      err => {
        this.isLoading = false;
      }
    );
  }

  controlStatusListener() {
    this.form.get('username').statusChanges.subscribe(status => {
      this.usernameControlIsValid = status === 'VALID';
    });

    this.form.get('password').statusChanges.subscribe(status => {
      this.passwordControlIsValid = status === 'VALID';
    });
  }

  onDone() {
    this.formService.dismiss([
      this.usernameEl.nativeElement,
      this.passwordEl.nativeElement
    ]);
  }
}
