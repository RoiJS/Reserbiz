import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { UIService } from '../services/ui.service';
import { SharedComponent } from '../shared/components/shared/shared.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent
  extends SharedComponent
  implements OnInit, OnDestroy, AfterViewInit, AfterViewChecked
{
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected uiService: UIService,
    private translateService: TranslateService
  ) {
    super(uiService);
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
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.COMPANY_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.COMPANY_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.COMPANY_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.USERNAME_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.USERNAME_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.USERNAME_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.PASSWORD_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.PASSWORD_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'LOGIN_PAGE.BODY.OVERVIEW_SECTION.TABLE_INFORMATION.PASSWORD_INFORMATION.DESCRIPTION'
        ),
      },
    ];
  }
}
