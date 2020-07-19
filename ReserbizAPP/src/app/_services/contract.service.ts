import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from './base.service';
import { Contract } from '../_models/contract.model';
import { ContractMapper } from '../_helpers/contract-mapper.helper';
import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IEntityFilter } from '../_interfaces/ientity-filter.interface';

@Injectable({ providedIn: 'root' })
export class ContractService extends BaseService<Contract>
  implements IBaseService<Contract> {
  private _loadContractListFlag = new BehaviorSubject<void>(null);

  constructor(public http: HttpClient) {
    super(new ContractMapper(), http);
  }

  getEntities(entityFilter: IEntityFilter): Observable<Contract[]> {
    const searchKeyword = entityFilter ? entityFilter.searchKeyword : '';
    return this.getEntitiesFromServer(
      `${this._apiBaseUrl}/contract/getAllContracts`
    );
  }

  deleteMultipleItems(contracts: Contract[]): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/tenant/deleteMultipleTenants`,
      contracts
    );
  }

  deleteItem(tenantId: number): Observable<void> {
    return this.deleteItemOnServer(
      `${this._apiBaseUrl}/tenant/deleteTenant?tenantId=${tenantId}`
    );
  }

  getContracts(tenantId: number): Observable<Contract[]> {
    return this.http
      .get<Contract[]>(
        `${this._apiBaseUrl}/contract/getAllContractsPerTenant/${tenantId}`
      )
      .pipe(
        map((tenants: Contract[]) => {
          return tenants.map((c: Contract) => {
            const contract = new Contract();

            contract.id = c.id;
            contract.code = c.code;
            contract.tenantId = c.tenantId;
            contract.termId = c.termId;
            contract.effectiveDate = c.effectiveDate;
            contract.isOpenContract = c.isOpenContract;
            contract.durationValue = c.durationValue;
            contract.expirationDate = c.expirationDate;
            contract.isExpired = c.isExpired;

            return contract;
          });
        })
      );
  }

  reloadListFlag() {
    this._loadContractListFlag.next();
  }

  get loadContractListFlag(): BehaviorSubject<void> {
    return this._loadContractListFlag;
  }
}
