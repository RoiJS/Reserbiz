import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

import { BaseService } from './base.service';
import { TermMiscellaneous } from '../_models/term-miscellaneous.model';
import { TermMiscellaneousMapper } from '../_helpers/term-miscellaneous-mapper.helper';

import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IEntityFilter } from '../_interfaces/ientity-filter.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';

@Injectable({
  providedIn: 'root',
})
export class TermMiscellaneousService extends BaseService<TermMiscellaneous>
  implements IBaseService<TermMiscellaneous> {
  private _loadTermMiscellaneousListFlag = new BehaviorSubject<void>(null);
  constructor(public http: HttpClient) {
    super(new TermMiscellaneousMapper(), http);
  }

  getEntities(entityFilter: IEntityFilter): Observable<TermMiscellaneous[]> {
    return this.getEntitiesFromServer(
      `${this._apiBaseUrl}/termMiscellaneous/getAllTermMiscellaneousPerTerm/${entityFilter.parentId}`
    );
  }

  getTermMiscellneous(
    termMiscellaneousId: number
  ): Observable<TermMiscellaneous> {
    return this.getEntityFromServer(
      `${this._apiBaseUrl}/termMiscellaneous/${termMiscellaneousId}`
    );
  }

  deleteMultipleItems(
    termMiscellaneous: TermMiscellaneous[]
  ): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/termMiscellaneous/deleteMultipleTermMiscellaneous`,
      termMiscellaneous
    );
  }

  deleteItem(termId: number): Observable<void> {
    return this.deleteItemOnServer(
      `${this._apiBaseUrl}/termMiscellaneous/${termId}`
    );
  }

  saveNewEntity(termMiscellaneousForCreate: IDtoProcess): Observable<void> {
    return this.saveNewEntityToServer(
      `${this._apiBaseUrl}/termMiscellaneous/create?termId=${termMiscellaneousForCreate.id}`,
      termMiscellaneousForCreate.dtoEntity
    );
  }

  updateEntity(termMiscellaneosUpdateDto: IDtoProcess): Observable<void> {
    return this.updateEntityToServer(
      `${this._apiBaseUrl}/termMiscellaneous/${termMiscellaneosUpdateDto.id}`,
      termMiscellaneosUpdateDto.dtoEntity
    );
  }

  reloadListFlag() {
    this._loadTermMiscellaneousListFlag.next();
  }

  get loadTermMiscellaneousListFlag(): BehaviorSubject<void> {
    return this._loadTermMiscellaneousListFlag;
  }
}
