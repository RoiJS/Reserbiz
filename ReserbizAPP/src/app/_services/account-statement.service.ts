import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';

import { BaseService } from './base.service';

import { AccountStatement } from '../_models/account-statement.model';
import { EntityPaginationList } from '../_models/entity-pagination-list.model';
import { AccountStatementPaginationList } from '../_models/account-statement-pagination-list.model';

import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IAccountStatementFilter } from '../_interfaces/iaccount-statement-filter.interface';

import { AccountStatementMapper } from '../_helpers/account-statement-mapper.helper';

@Injectable({ providedIn: 'root' })
export class AccountStatementService
  extends BaseService<AccountStatement>
  implements IBaseService<AccountStatement> {
  private _loadAccountStatementListFlag = new BehaviorSubject<void>(null);
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

  async getAccountStatement(
    accountStatementId: number
  ): Promise<AccountStatement> {
    return await this.getEntityFromServer(
      `${this._apiBaseUrl}/accountstatement/${accountStatementId}`
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

  reloadListFlag() {
    this._loadAccountStatementListFlag.next();
  }

  mapPaginatedEntityData(
    data: AccountStatementPaginationList
  ): EntityPaginationList {
    const accountStatementPaginationList = new AccountStatementPaginationList();

    accountStatementPaginationList.totalItems = data.totalItems;
    accountStatementPaginationList.totalExpectedAmount =
      data.totalExpectedAmount;
    accountStatementPaginationList.totalPaidAmount = data.totalPaidAmount;

    accountStatementPaginationList.page = data.page;
    accountStatementPaginationList.numberOfItemsPerPage =
      data.numberOfItemsPerPage;
    const items = data.items.map((d: AccountStatement) => {
      return this.mapper.mapEntity(d);
    });

    accountStatementPaginationList.items = items;

    return accountStatementPaginationList;
  }

  get loadAccountStatementListFlag(): BehaviorSubject<void> {
    return this._loadAccountStatementListFlag;
  }
}
