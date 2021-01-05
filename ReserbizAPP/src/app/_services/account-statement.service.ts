import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';

import { BaseService } from './base.service';

import { AccountStatement } from '../_models/account-statement.model';
import { AccountStatementsAmountSummary } from '../_models/account-statement-amount-summary.model';
import { EntityPaginationList } from '../_models/entity-pagination-list.model';
import { AccountStatementPaginationList } from '../_models/account-statement-pagination-list.model';

import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IAccountStatementFilter } from '../_interfaces/iaccount-statement-filter.interface';

import { AccountStatementMapper } from '../_helpers/account-statement-mapper.helper';

@Injectable({ providedIn: 'root' })
export class AccountStatementService
  extends BaseService<AccountStatement>
  implements IBaseService<AccountStatement> {
  private _loadAccountStatementListFlag = new BehaviorSubject<boolean>(false);
  constructor(public http: HttpClient) {
    super(new AccountStatementMapper(), http);
  }

  getPaginatedEntities(
    accountStatementFilter: IAccountStatementFilter
  ): Observable<EntityPaginationList> {
    const params = <IAccountStatementFilter>(
      this.parseRequestParams(accountStatementFilter.toFilterJSON())
    );

    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/accountstatement/getAccountStatementsPerContract`,
      params
    );
  }

  getUnpaidAccountStatements(): Observable<EntityPaginationList> {
    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/accountstatement/getUnpaidAccountStatements`
    );
  }

  async getAccountStatementsAmountSummary(): Promise<AccountStatementsAmountSummary> {
    return this.http
      .get<AccountStatementsAmountSummary>(
        `${this._apiBaseUrl}/accountstatement/getAccountStatementsAmountSummary`
      )
      .toPromise();
  }

  async getAccountStatement(
    accountStatementId: number
  ): Promise<AccountStatement> {
    return await this.getEntityFromServer(
      `${this._apiBaseUrl}/accountstatement/${accountStatementId}`
    ).toPromise();
  }

  async getFirstAccountStatement(
    contractId: number
  ): Promise<AccountStatement> {
    return await this.getEntityFromServer(
      `${this._apiBaseUrl}/accountstatement/getFirstAccountStatement/${contractId}`
    ).toPromise();
  }

  async updateWaterAndElectricBillAmount(
    id: number,
    waterBillAmount: number,
    electricBillAmount: number
  ): Promise<void> {
    return await this.http
      .post<void>(
        `${this._apiBaseUrl}/accountstatement/updateWaterAndElectricBillAmount`,
        {
          id,
          waterBillAmount,
          electricBillAmount,
        }
      )
      .toPromise();
  }

  reloadListFlag(reset: boolean) {
    this._loadAccountStatementListFlag.next(reset);
  }

  mapPaginatedEntityData(
    data: AccountStatementPaginationList
  ): EntityPaginationList {
    const accountStatementPaginationList = new AccountStatementPaginationList();

    accountStatementPaginationList.totalItems = data.totalItems;
    accountStatementPaginationList.totalExpectedAmount =
      data.totalExpectedAmount;
    accountStatementPaginationList.totalPaidAmount = data.totalPaidAmount;
    accountStatementPaginationList.totalExpectedDepositAmount =
      data.totalExpectedDepositAmount;
    accountStatementPaginationList.totalPaidAmountFromDeposit =
      data.totalPaidAmountFromDeposit;

    accountStatementPaginationList.page = data.page;
    accountStatementPaginationList.numberOfItemsPerPage =
      data.numberOfItemsPerPage;
    const items = data.items.map((d: AccountStatement) => {
      return this.mapper.mapEntity(d);
    });

    accountStatementPaginationList.items = items;

    return accountStatementPaginationList;
  }

  get loadAccountStatementListFlag(): BehaviorSubject<boolean> {
    return this._loadAccountStatementListFlag;
  }
}
