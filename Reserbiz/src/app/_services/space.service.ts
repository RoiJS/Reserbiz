import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { SpaceMapper } from '../_helpers/mappers/space-mapper.helper';

import { IBaseService } from '../_interfaces/services/ibase-service.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';
import { ISpaceFilter } from '../_interfaces/filters/ispace-filter.interface';

import { EntityPaginationList } from '../_models/pagination_list/entity-pagination-list.model';
import { SpaceOption } from '../_models/options/space-option.model';
import { Space } from '../_models/space.model';

import { BaseService } from './base.service';

@Injectable({ providedIn: 'root' })
export class SpaceService
  extends BaseService<Space>
  implements IBaseService<Space> {
  private _loadSpacesFlag = new BehaviorSubject<boolean>(false);
  constructor(public http: HttpClient) {
    super(new SpaceMapper(), http);
  }

  getPaginatedEntities(
    spaceFilter: ISpaceFilter
  ): Observable<EntityPaginationList> {
    const params = <ISpaceFilter>(
      this.parseRequestParams(spaceFilter.toFilterJSON())
    );

    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/space`,
      params
    );
  }

  getSpace(spaceId: number): Observable<Space> {
    return this.getEntityFromServer(`${this._apiBaseUrl}/space/${spaceId}`);
  }

  getSpacesAsOptions(
    translateService: TranslateService
  ): Observable<SpaceOption[]> {
    return this.http
      .get<SpaceOption[]>(`${this._apiBaseUrl}/space/getSpaceAsOptions`)
      .pipe(
        map((stos: SpaceOption[]) => {
          return stos.map((st: SpaceOption) => {
            const spaceOption = new SpaceOption();
            spaceOption.id = st.id;
            spaceOption.name = st.name;
            spaceOption.spaceTypeId = st.spaceTypeId;
            spaceOption.isNotOccupied = st.isNotOccupied;
            spaceOption.occupiedByContractId = st.occupiedByContractId;
            spaceOption.isDelete = st.isDelete;
            spaceOption.isActive = st.isActive;
            spaceOption.canBeSelected = st.canBeSelected;
            spaceOption.inactiveText = translateService.instant(
              'GENERAL_TEXTS.INACTIVE'
            );
            return spaceOption;
          });
        })
      );
  }

  deleteMultipleItems(spaces: Space[]): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/space/deleteMultipleSpaces`,
      spaces
    );
  }

  deleteItem(spaceId: number): Observable<void> {
    return this.deleteItemOnServer(`${this._apiBaseUrl}/space/${spaceId}`);
  }

  saveNewEntity(spaceForCreate: IDtoProcess): Observable<void> {
    return this.saveNewEntityToServer(
      `${this._apiBaseUrl}/space/create`,
      spaceForCreate.dtoEntity
    );
  }

  updateEntity(spaceForUpdate: IDtoProcess): Observable<void> {
    return this.updateEntityToServer(
      `${this._apiBaseUrl}/space/${spaceForUpdate.id}`,
      spaceForUpdate.dtoEntity
    );
  }

  async getAvailableSpacesCount(): Promise<number> {
    return this.http
      .get<number>(`${this._apiBaseUrl}/space/getAvailableSpacesCount`)
      .toPromise();
  }

  reloadListFlag(reset: boolean) {
    this._loadSpacesFlag.next(reset);
  }

  get loadSpacesFlag(): BehaviorSubject<boolean> {
    return this._loadSpacesFlag;
  }
}
