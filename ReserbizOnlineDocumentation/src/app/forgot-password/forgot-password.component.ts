import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { UIService } from '../services/ui.service';

import { SharedComponent } from '../shared/components/shared/shared.component';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent
  extends SharedComponent
  implements OnInit, OnDestroy, AfterViewInit, AfterViewChecked
{
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected uiService: UIService,
    private translateService: TranslateService
  ) {
    super(uiService);
    this._fragment = '';
    this.activatedRoute = activatedRoute;
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.setDatasource();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

  ngAfterViewChecked(): void {
    super.ngAfterViewChecked();
  }

  setDatasource(): void {
    this.dataSource = [
      {
        name: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.AUTHENTICATE_USER_SECTION.TABLE_INFORMATION.COMPANY_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.AUTHENTICATE_USER_SECTION.TABLE_INFORMATION.COMPANY_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.AUTHENTICATE_USER_SECTION.TABLE_INFORMATION.COMPANY_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.AUTHENTICATE_USER_SECTION.TABLE_INFORMATION.EMAIL_ADDRESS_OR_USERNAME_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.AUTHENTICATE_USER_SECTION.TABLE_INFORMATION.EMAIL_ADDRESS_OR_USERNAME_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.AUTHENTICATE_USER_SECTION.TABLE_INFORMATION.EMAIL_ADDRESS_OR_USERNAME_INFORMATION.DESCRIPTION'
        ),
      },
    ];

    this.dataSource2 = [
      {
        name: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.CHANGE_PASSWORD_SECTION.TABLE_INFORMATION.NEW_PASSWORD_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.CHANGE_PASSWORD_SECTION.TABLE_INFORMATION.NEW_PASSWORD_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.CHANGE_PASSWORD_SECTION.TABLE_INFORMATION.NEW_PASSWORD_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.CHANGE_PASSWORD_SECTION.TABLE_INFORMATION.CONFIRM_PASSWORD_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.CHANGE_PASSWORD_SECTION.TABLE_INFORMATION.CONFIRM_PASSWORD_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'FORGOT_PASSWORD_PAGE.BODY.CHANGE_PASSWORD_SECTION.TABLE_INFORMATION.CONFIRM_PASSWORD_INFORMATION.DESCRIPTION'
        ),
      },
    ];
  }
}
