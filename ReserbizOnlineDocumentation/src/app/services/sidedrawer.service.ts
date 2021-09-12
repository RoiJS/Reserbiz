import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { IMenu } from '../_interfaces/imenu';

@Injectable({
  providedIn: 'root',
})
export class SidedrawerService {
  mainMenu: IMenu[] = [];
  constructor() {
    this.mainMenu = [
      {
        name: 'MAIN_MENU.SYSTEM_OVERVIEW',
        url: '/system-overview',
      },
      {
        name: 'MAIN_MENU.SYSTEM_NAVIGATION',
        url: '/system-navigation',
      },
      {
        name: 'MAIN_MENU.LOGIN',
        url: '/login',
      },
      {
        name: 'MAIN_MENU.FORGOT_PASSWORD',
        url: '/forgot-password',
        children: [
          {
            name: 'FORGOT_PASSWORD_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/forgot-password',
            fragment: 'overview-section',
          },
          {
            name: 'FORGOT_PASSWORD_PAGE.BODY.AUTHENTICATE_USER_SECTION.TITLE',
            url: '/forgot-password',
            fragment: 'authenticate-user-section',
          },
          {
            name: 'FORGOT_PASSWORD_PAGE.BODY.CHANGE_PASSWORD_SECTION.TITLE',
            url: '/forgot-password',
            fragment: 'change-password-section',
          },
        ],
      },
      {
        name: 'MAIN_MENU.DASHBOARD',
        url: '/dashboard',
        children: [
          {
            name: 'DASHBAORD_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/dashboard',
            fragment: 'overview-section',
          },
          {
            name: 'DASHBAORD_PAGE.BODY.ACTIVE_TENANTS_WIDGET_SECTION.TITLE',
            url: '/dashboard',
            fragment: 'active-tenants-widget-section',
          },
          {
            name: 'DASHBAORD_PAGE.BODY.ACTIVE_UNITS_WIDGET_SECTION.TITLE',
            url: '/dashboard',
            fragment: 'active-units-widget-section',
          },
          {
            name: 'DASHBAORD_PAGE.BODY.ACTIVE_CONTRACTS_WIDGET_SECTION.TITLE',
            url: '/dashboard',
            fragment: 'active-contracts-widget-section',
          },
          {
            name: 'DASHBAORD_PAGE.BODY.UPCOMING_DUE_DATES_WIDGET_SECTION.TITLE',
            url: '/dashboard',
            fragment: 'upcoming-due-dates-widget-section',
          },
          {
            name: 'DASHBAORD_PAGE.BODY.ACCOUNT_STATEMENTS_AMOUNT_SUMMARY_WIDGET_SECTION.TITLE',
            url: '/dashboard',
            fragment: 'account-statement-summary-amount-widget-section',
          },
          {
            name: 'DASHBAORD_PAGE.BODY.UNPAID_ACCOUNT_STATEMENTS_WIDGET_SECTION.TITLE',
            url: '/dashboard',
            fragment: 'unpaid-account-statements-widget-section',
          },
        ],
      },
      {
        name: 'MAIN_MENU.TENANTS',
        url: '/tenants',
        children: [
          {
            name: 'TENANTS_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/tenants',
            fragment: 'overview-section',
          },
          {
            name: 'TENANTS_PAGE.BODY.SEARCH_TENANTS_SECTION.TITLE',
            url: '/tenants',
            fragment: 'search-tenants-section',
          },
          {
            name: 'TENANTS_PAGE.BODY.ADD_NEW_TENANT_SECTION.TITLE',
            url: '/tenants',
            fragment: 'add-new-tenant-section',
          },
          {
            name: 'TENANTS_PAGE.BODY.VIEW_TENANT_INFORMATION_SECTION.TITLE',
            url: '/tenants',
            fragment: 'view-tenant-information-section',
          },
          {
            name: 'TENANTS_PAGE.BODY.EDIT_TENANT_INFORMATION_SECTION.TITLE',
            url: '/tenants',
            fragment: 'edit-tenant-information-section',
          },
          {
            name: 'TENANTS_PAGE.BODY.MANAGE_CONTACT_PERSONS_LIST_SECTION.TITLE',
            url: '/tenants',
            fragment: 'manage-contact-person-list-section',
          },
          {
            name: 'TENANTS_PAGE.BODY.DELETE_TENANT_SECTION.TITLE',
            url: '/tenants',
            fragment: 'delete-tenant-section',
          },
          {
            name: 'TENANTS_PAGE.BODY.ACTIVATE_DEACTIVATE_TENANT_SECTION.TITLE',
            url: '/tenants',
            fragment: 'activate-deactivate-tenant-section',
          },
        ],
      },
      {
        name: 'MAIN_MENU.UNIT_TYPES',
        url: '/unit-types',
        children: [
          {
            name: 'UNIT_TYPES_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/unit-types',
            fragment: 'overview-section',
          },
          {
            name: 'UNIT_TYPES_PAGE.BODY.ADD_NEW_UNIT_TYPE_SECTION.TITLE',
            url: '/unit-types',
            fragment: 'add-new-unit-type-section',
          },
          {
            name: 'UNIT_TYPES_PAGE.BODY.EDIT_UNIT_TYPE_SECTION.TITLE',
            url: '/unit-types',
            fragment: 'edit-unit-type-section',
          },
          {
            name: 'UNIT_TYPES_PAGE.BODY.SEARCH_UNIT_TYPE_SECTION.TITLE',
            url: '/unit-types',
            fragment: 'search-unit-type-section',
          },
          {
            name: 'UNIT_TYPES_PAGE.BODY.DELETE_UNIT_TYPE_SECTION.TITLE',
            url: '/unit-types',
            fragment: 'delete-unit-type-section',
          },
        ],
      },
      {
        name: 'MAIN_MENU.UNITS',
        url: '/units',
        children: [
          {
            name: 'UNITS_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/units',
            fragment: 'overview-section',
          },
          {
            name: 'UNITS_PAGE.BODY.ADD_NEW_UNIT_SECTION.TITLE',
            url: '/units',
            fragment: 'add-new-type-section',
          },
          {
            name: 'UNITS_PAGE.BODY.EDIT_UNIT_SECTION.TITLE',
            url: '/units',
            fragment: 'edit-unit-section',
          },
          {
            name: 'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TITLE',
            url: '/units',
            fragment: 'filter-unit-section',
          },
          {
            name: 'UNITS_PAGE.BODY.DELETE_UNIT_SECTION.TITLE',
            url: '/units',
            fragment: 'delete-unit-section',
          },
        ],
      },
      {
        name: 'MAIN_MENU.TERMS',
        url: '/terms',
        children: [
          {
            name: 'TERMS_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/terms',
            fragment: 'overview-section',
          },
          {
            name: 'TERMS_PAGE.BODY.ADD_NEW_TERM_SECTION.TITLE',
            url: '/terms',
            fragment: 'add-new-term-section',
          },
          {
            name: 'TERMS_PAGE.BODY.MANAGE_TERM_MISCELLANEOUS_SECTION.TITLE',
            url: '/terms',
            fragment: 'manage-term-miscellaneous-section',
          },
          {
            name: 'TERMS_PAGE.BODY.VIEW_TERM_INFORMATION_SECTION.TITLE',
            url: '/terms',
            fragment: 'view-term-section',
          },
          {
            name: 'TERMS_PAGE.BODY.EDIT_TERM_INFORMATION_SECTION.TITLE',
            url: '/terms',
            fragment: 'edit-term-section',
          },
          {
            name: 'TERMS_PAGE.BODY.DELETE_TERM_SECTION.TITLE',
            url: '/terms',
            fragment: 'delete-term-section',
          },
          {
            name: 'TERMS_PAGE.BODY.SEARCH_TERMS_SECTION.TITLE',
            url: '/terms',
            fragment: 'search-term-section',
          },
        ],
      },
      {
        name: 'MAIN_MENU.CONTRACTS',
        url: '/contracts',
        children: [
          {
            name: 'CONTRACTS_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/contracts',
            fragment: 'overview-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.CONTRACT_LIST_SECTION.TITLE',
            url: '/contracts',
            fragment: 'contract-list-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.SEARCH_CONTRACT_SECTION.TITLE',
            url: '/contracts',
            fragment: 'search-contracts-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.ARCHIVE_CONTRACT_SECTION.TITLE',
            url: '/contracts',
            fragment: 'archive-contract-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.ARCHIVED_CONTRACT_LIST_SECTION.TITLE',
            url: '/contracts',
            fragment: 'archived-contract-list-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.SEARCH_ARCHIVED_CONTRACTS_SECTION.TITLE',
            url: '/contracts',
            fragment: 'search-archived-contracts-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.UNARCHIVE_OR_ACTIVATE_CONTRACTS_SECTION.TITLE',
            url: '/contracts',
            fragment: 'unarchive-activate-contracts-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.CALENDAR_VIEW_SECTION.TITLE',
            url: '/contracts',
            fragment: 'calendar-view-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.CREATE_NEW_CONTRACT_SECTION.TITLE',
            url: '/contracts',
            fragment: 'create-new-contract-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.CONTRACT_DETAILS_PAGE_SECTION.TITLE',
            url: '/contracts',
            fragment: 'contract-details-page-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.ENCASH_DEPOSIT_AMOUNT_SECTION.TITLE',
            url: '/contracts',
            fragment: 'encash-deposit-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.RETURN_ENCASHED_DEPOSIT_AMOUNT_SECTION.TITLE',
            url: '/contracts',
            fragment: 'return-encashed-deposit-section',
          },
          {
            name: 'CONTRACTS_PAGE.BODY.EDIT_CONTRACT_DETAILS_SECTION.TITLE',
            url: '/contracts',
            fragment: 'edit-contract-section',
          },
          {
            name: 'MAIN_MENU.STATEMENT_OF_ACCOUNTS',
            url: '/contracts/statement-of-accounts',
            children: [
              {
                name: 'STATEMENT_OF_ACCOUNTS_PAGE.BODY.OVERVIEW_SECTION.TITLE',
                url: '/contracts/statement-of-accounts',
                fragment: 'overview-section',
              },
              {
                name: 'STATEMENT_OF_ACCOUNTS_PAGE.BODY.ADD_NEW_STATEMENT_OF_ACCOUNTS_SECTION.TITLE',
                url: '/contracts/statement-of-accounts',
                fragment: 'add-new-statement-of-account-section',
              },
              {
                name: 'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEARCH_STATEMENT_OF_ACCOUNTS_SECTION.TITLE',
                url: '/contracts/statement-of-accounts',
                fragment: 'search-statement-of-account-section',
              },
              {
                name: 'STATEMENT_OF_ACCOUNTS_PAGE.BODY.STATEMENT_OF_ACCOUNT_DETAILS_SECTION.TITLE',
                url: '/contracts/statement-of-accounts',
                fragment: 'statement-of-account-details-section',
              },
              {
                name: 'STATEMENT_OF_ACCOUNTS_PAGE.BODY.DELETE_STATEMENT_OF_ACCOUNT.TITLE',
                url: '/contracts/statement-of-accounts',
                fragment: 'delete-statement-of-account-section',
              },
              {
                name: 'STATEMENT_OF_ACCOUNTS_PAGE.BODY.SEND_STATEMENT_OF_ACCOUNTS_DETAILS.TITLE',
                url: '/contracts/statement-of-accounts',
                fragment: 'send-statement-of-account-details-section',
              },
              {
                name: 'MAIN_MENU.PAYMENTS',
                url: '/contracts/statement-of-accounts/payments',
                children: [
                  {
                    name: 'PAYMENTS_PAGE.BODY.OVERVIEW_SECTION.TITLE',
                    url: '/contracts/statement-of-accounts/payments',
                    fragment: 'overview-section',
                  },
                  {
                    name: 'PAYMENTS_PAGE.BODY.REGISTER_PAYMENT_SECTION.TITLE',
                    url: '/contracts/statement-of-accounts/payments',
                    fragment: 'register-payment-section',
                  },
                  {
                    name: 'PAYMENTS_PAGE.BODY.VIEW_PAYMENT_DETAILS_SECTION.TITLE',
                    url: '/contracts/statement-of-accounts/payments',
                    fragment: 'view-payment-details-section',
                  },
                  {
                    name: 'PAYMENTS_PAGE.BODY.FILTER_PAYMENT_LIST_SECTION.TITLE',
                    url: '/contracts/statement-of-accounts/payments',
                    fragment: 'filter-payment-list-section',
                  },
                ],
              },
              {
                name: 'MAIN_MENU.PENALTIES',
                url: '/contracts/statement-of-accounts/penalties',
                children: [
                  {
                    name: 'PENALTIES_PAGE.BODY.OVERVIEW_SECTION.TITLE',
                    url: '/contracts/statement-of-accounts/penalties',
                    fragment: 'overview-section',
                  },
                  {
                    name: 'PENALTIES_PAGE.BODY.SORT_PENALTY_LIST_SECTION.TITLE',
                    url: '/contracts/statement-of-accounts/penalties',
                    fragment: 'sort-penalty-list-section',
                  },
                ],
              },
            ],
          },
        ],
      },
      {
        name: 'MAIN_MENU.PROFILE',
        url: '/profile',
      },
      {
        name: 'MAIN_MENU.NOTIFICATIONS',
        url: '/notifications',
        children: [
          {
            name: 'NOTIFICATION_PAGE.BODY.OVERVIEW_SECTION.TITLE',
            url: '/notifications',
            fragment: 'overview-section',
          },
          {
            name: 'NOTIFICATION_PAGE.BODY.FILTER_NOTIFICATION_SECTION.TITLE',
            url: '/notifications',
            fragment: 'filter-notification-section',
          },
        ],
      },
      {
        name: 'MAIN_MENU.SETTINGS',
        url: '/settings',
      },
    ];
  }
}
