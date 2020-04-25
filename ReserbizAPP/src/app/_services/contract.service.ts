import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@src/environments/environment';
import { map } from 'rxjs/operators';

import { Contract } from '../_models/contract.model';

@Injectable({ providedIn: 'root' })
export class ContractService {
  private _apiBaseUrl = environment.reserbizAPIEndPoint;

  constructor(private http: HttpClient) {}

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
            contract.status = c.status;
            contract.expirationDate = c.expirationDate;
            contract.isExpired = c.isExpired;

            return contract;
          });
        })
      );
  }
}
