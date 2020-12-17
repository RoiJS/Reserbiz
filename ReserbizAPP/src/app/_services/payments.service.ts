import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { PaymentMapper } from '../_helpers/payment-mapper.helper';

import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IPaymentFilter } from '../_interfaces/ipayment-filter.interface';
import { EntityPaginationList } from '../_models/entity-pagination-list.model';

import { Payment } from '../_models/payment.model';

import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class PaymentsService
  extends BaseService<Payment>
  implements IBaseService<Payment> {
  private _loadPaymentListFlag = new BehaviorSubject<void>(null);

  constructor(public http: HttpClient) {
    super(new PaymentMapper(), http);
  }

  getPaginatedEntities(
    paymentFilter: IPaymentFilter
  ): Observable<EntityPaginationList> {
    const params = <IPaymentFilter>(
      this.parseRequestParams(paymentFilter.toFilterJSON())
    );

    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/paymentbreakdown/getPaymentsPerAccountStatement`,
      params
    );
  }

  async getPaymentDetails(paymentId: number): Promise<Payment> {
    return await this.getEntityFromServer(
      `${this._apiBaseUrl}/paymentbreakdown/${paymentId}`
    ).toPromise();
  }

  reloadListFlag() {
    this._loadPaymentListFlag.next();
  }

  get loadPaymentListFlag(): BehaviorSubject<void> {
    return this._loadPaymentListFlag;
  }
}
