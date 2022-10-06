import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { PenaltyMapper } from '../_helpers/mappers/penalty-mapper.helper';
import { IBaseService } from '../_interfaces/services/ibase-service.interface';
import { IPenaltyFilter } from '../_interfaces/filters/ipenalty-filter.interface';
import { EntityPaginationList } from '../_models/pagination_list/entity-pagination-list.model';
import { PenaltyPaginationList } from '../_models/pagination_list/penalty-pagination-list.model';
import { Penalty } from '../_models/penalty.model';
import { BaseService } from './base.service';

@Injectable({ providedIn: 'root' })
export class PenaltyService
  extends BaseService<Penalty>
  implements IBaseService<Penalty> {
  private _loadPenaltyListFlag = new BehaviorSubject<boolean>(false);

  constructor(public http: HttpClient) {
    super(new PenaltyMapper(), http);
  }

  getPaginatedEntities(
    paymentFilter: IPenaltyFilter
  ): Observable<EntityPaginationList> {
    const params = <IPenaltyFilter>(
      this.parseRequestParams(paymentFilter.toFilterJSON())
    );

    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/penaltybreakdown/getPenaltiesPerAccountStatement`,
      params
    );
  }

  mapPaginatedEntityData(data: PenaltyPaginationList): EntityPaginationList {
    const penaltyPaginationList = new PenaltyPaginationList();

    penaltyPaginationList.totalItems = data.totalItems;
    penaltyPaginationList.totalAmount = data.totalAmount;
    penaltyPaginationList.page = data.page;
    penaltyPaginationList.numberOfItemsPerPage = data.numberOfItemsPerPage;
    const items = data.items.map((p: Penalty) => {
      return this.mapper.mapEntity(p);
    });

    penaltyPaginationList.items = items;

    return penaltyPaginationList;
  }

  reloadListFlag(reset: boolean) {
    this._loadPenaltyListFlag.next(reset);
  }

  get loadPenaltyListFlag(): BehaviorSubject<boolean> {
    return this._loadPenaltyListFlag;
  }
}
