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
  selector: 'app-terms',
  templateUrl: './terms.component.html',
  styleUrls: ['./terms.component.scss'],
})
export class TermsComponent
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
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.CODE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.CODE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.CODE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.NAME_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.NAME_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.NAME_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_TYPE.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_TYPE.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_TYPE.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.RATE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.RATE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.RATE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.MAX_NUMBER_OF_OCCUPANTS.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.MAX_NUMBER_OF_OCCUPANTS.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.MAX_NUMBER_OF_OCCUPANTS.DESCRIPTION'
        ),
      },
    ];

    this.dataSource2 = [
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DURATION_UNIT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DURATION_UNIT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DURATION_UNIT_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.ADVANCED_PAYMENT_DURATION_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.ADVANCED_PAYMENT_DURATION_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.ADVANCED_PAYMENT_DURATION_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.ADVANCED_PAYMENT_DURATION_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.ADVANCED_PAYMENT_DURATION_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DEPOSIT_PAYMENT_DURATION_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DEPOSIT_PAYMENT_DURATION_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DEPOSIT_PAYMENT_DURATION_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DEPOSIT_PAYMENT_DURATION_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.DURATION_INFORMATION_SECTION.DEPOSIT_PAYMENT_DURATION_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
    ];

    this.dataSource3 = [
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.EXCLUDE_ELECTRIC_BILL_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.EXCLUDE_ELECTRIC_BILL_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.EXCLUDE_ELECTRIC_BILL_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.ELECTRIC_BILL_AMOUNT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.ELECTRIC_BILL_AMOUNT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.ELECTRIC_BILL_AMOUNT_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.EXCLUDE_WATER_BILL_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.EXCLUDE_WATER_BILL_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.EXCLUDE_WATER_BILL_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.WATER_BILL_AMOUNT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.WATER_BILL_AMOUNT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.ELECTRIC_AND_WATER_BILL_DETAILS_SECTION.WATER_BILL_AMOUNT_INFORMATION.DESCRIPTION'
        ),
      },
    ];

    this.dataSource4 = [
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_TYPE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_TYPE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_TYPE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_INFORMATION.DESCRIPTION_3'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_INFORMATION.DESCRIPTION_4'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_VALUE_INFORMATION.DESCRIPTION_5'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_AMOUNT_PER_DURATION_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_AMOUNT_PER_DURATION_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_AMOUNT_PER_DURATION_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_AMOUNT_PER_DURATION_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_AMOUNT_PER_DURATION_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_VALUE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_VALUE_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_VALUE_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_VALUE_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_VALUE_INFORMATION.DESCRIPTION_3'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.PENALTY_DETAILS_SECTION.PENALTY_EFFECTIVE_AFTER_DURATION_VALUE_INFORMATION.DESCRIPTION_4'
          )}
        `,
      },
    ];

    this.dataSource5 = [
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.GENERATE_ACCOUNT_STATEMENTS_DAYS_BEFORE_DUE_DATE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.GENERATE_ACCOUNT_STATEMENTS_DAYS_BEFORE_DUE_DATE_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.GENERATE_ACCOUNT_STATEMENTS_DAYS_BEFORE_DUE_DATE_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.GENERATE_ACCOUNT_STATEMENTS_DAYS_BEFORE_DUE_DATE_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.GENERATE_ACCOUNT_STATEMENTS_DAYS_BEFORE_DUE_DATE_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
    ];
  }
}