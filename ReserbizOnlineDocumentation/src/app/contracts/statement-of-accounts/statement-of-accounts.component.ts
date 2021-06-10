import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { UIService } from 'src/app/services/ui.service';
import { SharedComponent } from 'src/app/shared/components/shared/shared.component';

@Component({
  selector: 'app-statement-of-accounts',
  templateUrl: './statement-of-accounts.component.html',
  styleUrls: ['./statement-of-accounts.component.scss'],
})
export class StatementOfAccountsComponent
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
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.FROM_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.FROM_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.FROM_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.FROM_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.TO_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.TO_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.TO_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.TO_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.PAYMENT_STATUS_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.PAYMENT_STATUS_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.PAYMENT_STATUS_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.PAYMENT_STATUS_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.DESCRIPTION'
        ),
      },
    ];
  }
}
