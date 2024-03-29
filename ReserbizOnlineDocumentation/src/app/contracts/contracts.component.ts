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
  selector: 'app-contracts',
  templateUrl: './contracts.component.html',
  styleUrls: ['./contracts.component.scss'],
})
export class ContractsComponent
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
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.TENANT_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.TENANT_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.TENANT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.TENANT_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_FROM_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_FROM_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_FROM_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_FROM_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_TO_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_TO_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_TO_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.ACTIVE_TO_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_FROM_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_FROM_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_FROM_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_FROM_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_TO_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_TO_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_TO_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.NEXT_DUE_DATE_TO_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.OPEN_CONTRACT_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.OPEN_CONTRACT_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.OPEN_CONTRACT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TABLE_INFORMATION.SORT_ORDER_INFORMATION.DESCRIPTION'
        ),
      },
    ];

    this.dataSource2 = [
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.CODE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.CODE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.CODE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.TENANT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.TENANT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.TENANT_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.TERM_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.TERM_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.TERM_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_TYPE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_TYPE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_TYPE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.UNIT_INFORMATION.DESCRIPTION_2'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.EFFECTIVE_DATE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.EFFECTIVE_DATE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.EFFECTIVE_DATE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.OPEN_CONTRACT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.OPEN_CONTRACT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.OPEN_CONTRACT_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_UNIT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_UNIT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_UNIT_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_VALUE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_VALUE_INFORMATION.DATA_TYPE'
        ),
        definition: `
        ${this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_VALUE_INFORMATION.DESCRIPTION_1'
        )}
        ${this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_VALUE_INFORMATION.DESCRIPTION_2'
        )}
        ${this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.GENERAL_INFORMATION_SECTION.DURATION_VALUE_INFORMATION.DESCRIPTION_3'
        )}
        `,
      },
    ];

    this.dataSource9 = [
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_RENTAL_FEE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_RENTAL_FEE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_RENTAL_FEE_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_MISCELLANEOUS_FEES_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_MISCELLANEOUS_FEES_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_MISCELLANEOUS_FEES_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_UTILITY_BILLS_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_UTILITY_BILLS_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_UTILITY_BILLS_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_PENALTY_AMOUNT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_PENALTY_AMOUNT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TABLE_INFORMATION.MARK_ACCOUNT_STATEMENT_AS_PAID_SECTION.CHECK_PENALTY_AMOUNT_INFORMATION.DESCRIPTION'
        ),
      },
    ];

    this.dataSource3 = [
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

    this.dataSource4 = [
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

    this.dataSource5 = [
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.EXCLUDE_ELECTRIC_BILL_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.EXCLUDE_ELECTRIC_BILL_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.EXCLUDE_ELECTRIC_BILL_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.ELECTRIC_BILL_AMOUNT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.ELECTRIC_BILL_AMOUNT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.ELECTRIC_BILL_AMOUNT_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.EXCLUDE_WATER_BILL_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.EXCLUDE_WATER_BILL_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.EXCLUDE_WATER_BILL_INFORMATION.DESCRIPTION'
        ),
      },
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.WATER_BILL_AMOUNT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.WATER_BILL_AMOUNT_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.UTILITY_BILL_DETAILS_SECTION.WATER_BILL_AMOUNT_INFORMATION.DESCRIPTION'
        ),
      },
    ];

    this.dataSource6 = [
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

    this.dataSource7 = [
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
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.AUTO_SEND_STATEMENT_OF_ACCOUNT_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.AUTO_SEND_STATEMENT_OF_ACCOUNT_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.AUTOMATION_SETTINGS_SECTION.AUTO_SEND_STATEMENT_OF_ACCOUNT_INFORMATION.DESCRIPTION'
          )}
        `,
      },
    ];

    this.dataSource8 = [
      {
        name: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.MISCELLANEOUS_SETTINGS_SECTION.MISCELLANEOUS_DUE_DATE_INFORMATION.NAME'
        ),
        datatype: this.translateService.instant(
          'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.MISCELLANEOUS_SETTINGS_SECTION.MISCELLANEOUS_DUE_DATE_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.MISCELLANEOUS_SETTINGS_SECTION.MISCELLANEOUS_DUE_DATE_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.MISCELLANEOUS_SETTINGS_SECTION.MISCELLANEOUS_DUE_DATE_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TABLE_INFORMATION.MISCELLANEOUS_SETTINGS_SECTION.MISCELLANEOUS_DUE_DATE_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
    ];

    this.dataSource10 = [
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DATA_TYPE'
        ),
        definition: `
          ${this.translateService.instant(
            'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DESCRIPTION_1'
          )}
          ${this.translateService.instant(
            'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.CODE_SORT_ORDER_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.CODE_SORT_ORDER_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.CODE_SORT_ORDER_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TABLE_INFORMATION.CODE_SORT_ORDER_INFORMATION.DESCRIPTION'
        ),
      },
    ];
  }
}
