import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { PaymentMapper } from '../_helpers/mappers/payment-mapper.helper';

import { IBaseService } from '../_interfaces/services/ibase-service.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';
import { IPaymentFilter } from '../_interfaces/filters/ipayment-filter.interface';

import { EntityPaginationList } from '../_models/pagination_list/entity-pagination-list.model';
import { PaymentPaginationList } from '../_models/pagination_list/payment-pagination-list.model';

import { Payment } from '../_models/payment.model';

import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class PaymentsService
  extends BaseService<Payment>
  implements IBaseService<Payment>
{
  private _loadPaymentListFlag = new BehaviorSubject<boolean>(false);

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

  saveNewEntity(paymentForCreate: IDtoProcess): Observable<void> {
    return this.saveNewEntityToServer(
      `${this._apiBaseUrl}/paymentbreakdown/addPayment?accountStatementId=${paymentForCreate.id}`,
      paymentForCreate.dtoEntity
    );
  }

  mapPaginatedEntityData(data: PaymentPaginationList): EntityPaginationList {
    const paymentPaginationList = new PaymentPaginationList();

    paymentPaginationList.totalItems = data.totalItems;
    paymentPaginationList.totalAmount = data.totalAmount;
    paymentPaginationList.suggestedRentalAmount = data.suggestedRentalAmount;
    paymentPaginationList.suggestedElectricBillAmount =
      data.suggestedElectricBillAmount;
    paymentPaginationList.suggestedWaterBillAmount =
      data.suggestedWaterBillAmount;
    paymentPaginationList.suggestedMiscelleneousAmount =
      data.suggestedMiscelleneousAmount;
    paymentPaginationList.suggestedPenaltyAmount = data.suggestedPenaltyAmount;
    paymentPaginationList.depositedAmountBalance = data.depositedAmountBalance;
    paymentPaginationList.totalAmountFromDeposit = data.totalAmountFromDeposit;

    paymentPaginationList.totalExpectedRentalAmount =
      data.totalExpectedRentalAmount;
    paymentPaginationList.totalExpectedElectricBillAmount =
      data.totalExpectedElectricBillAmount;
    paymentPaginationList.totalExpectedWaterBillAmount =
      data.totalExpectedWaterBillAmount;
    paymentPaginationList.totalExpectedMiscellaneousFeesAmount =
      data.totalExpectedMiscellaneousFeesAmount;
    paymentPaginationList.totalExpectedPenaltyAmount =
      data.totalExpectedPenaltyAmount;

    paymentPaginationList.totalPaidRentalAmount = data.totalPaidRentalAmount;
    paymentPaginationList.totalPaidElectricBillAmount =
      data.totalPaidElectricBillAmount;
    paymentPaginationList.totalPaidWaterBillAmount =
      data.totalPaidWaterBillAmount;
    paymentPaginationList.totalPaidMiscellaneousFeesAmount =
      data.totalPaidMiscellaneousFeesAmount;
    paymentPaginationList.totalPaidPenaltyAmount = data.totalPaidPenaltyAmount;

    paymentPaginationList.page = data.page;
    paymentPaginationList.numberOfItemsPerPage = data.numberOfItemsPerPage;
    const items = data.items.map((p: Payment) => {
      return this.mapper.mapEntity(p);
    });

    paymentPaginationList.items = items;

    return paymentPaginationList;
  }

  reloadListFlag(reset: boolean) {
    this._loadPaymentListFlag.next(reset);
  }

  get loadPaymentListFlag(): BehaviorSubject<boolean> {
    return this._loadPaymentListFlag;
  }
}
