import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';

import { BaseService } from './base.service';
import { Contract } from '../_models/contract.model';
import { ContractMapper } from '../_helpers/contract-mapper.helper';
import { IBaseService } from '../_interfaces/ibase-service.interface';
import { EntityPaginationList } from '../_models/entity-pagination-list.model';
import { IContractFilter } from '../_interfaces/icontract-filter.interface';
import { ContractPaginationList } from '../_models/contract-pagination-list.model';

@Injectable({ providedIn: 'root' })
export class ContractService extends BaseService<Contract>
  implements IBaseService<Contract> {
  private _loadContractListFlag = new BehaviorSubject<void>(null);

  constructor(public http: HttpClient) {
    super(new ContractMapper(), http);
  }

  getPaginatedEntities(
    contractFilter: IContractFilter
  ): Observable<EntityPaginationList> {
    const params = this.parseRequestParams(contractFilter);
    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/contract/getAllContracts`,
      params
    );
  }

  deleteMultipleItems(contracts: Contract[]): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/contract/deleteMultipleContracts`,
      contracts
    );
  }

  deleteItem(contractId: number): Observable<void> {
    return this.deleteItemOnServer(
      `${this._apiBaseUrl}/contract/deleteContract?contractId=${contractId}`
    );
  }

  setEntityStatus(contractId: number, status: boolean): Observable<void> {
    return this.setEntityStatusOnServer(
      `${this._apiBaseUrl}/contract/setStatus/${contractId}/${status}`
    );
  }

  setMultipleEntityStatus(
    contracts: Contract[],
    status: boolean
  ): Observable<void> {
    return this.setMultipleEntityStatusOnServer(
      `${this._apiBaseUrl}/contract/setMultipleContractsStatus/${status}`,
      contracts,
      status
    );
  }

  reloadListFlag() {
    this._loadContractListFlag.next();
  }

  mapPaginatedEntityData(data: ContractPaginationList): EntityPaginationList {
    const contractPaginationList = new ContractPaginationList();

    contractPaginationList.totalItems = data.totalItems;
    contractPaginationList.totalNumberOfOpenContracts =
      data.totalNumberOfOpenContracts;
    contractPaginationList.page = data.page;
    contractPaginationList.numberOfItemsPerPage = data.numberOfItemsPerPage;
    const items = data.items.map((d: Contract) => {
      return this.mapper.mapEntity(d);
    });

    contractPaginationList.items = items;

    return contractPaginationList;
  }

  get loadContractListFlag(): BehaviorSubject<void> {
    return this._loadContractListFlag;
  }
}
