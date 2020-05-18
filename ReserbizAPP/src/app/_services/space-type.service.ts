import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';

import { environment } from '@src/environments/environment';

import { BaseService } from './base.service';
import { SpaceType } from '../_models/space-type.model';
import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IEntityFilter } from '../_interfaces/ientity-filter.interface';
import { SpaceTypeMapper } from '../_helpers/space-type-mapper.helper';
import { IDtoProcess } from '../_interfaces/idto-process.interface';

@Injectable({ providedIn: 'root' })
export class SpaceTypeService extends BaseService<SpaceType>
  implements IBaseService<SpaceType> {
  private _apiBaseUrl = environment.reserbizAPIEndPoint;
  private _loadSpaceTypesFlag = new BehaviorSubject<void>(null);

  constructor(public http: HttpClient) {
    super(new SpaceTypeMapper(), http);
  }

  getSpaceType(spaceTypeId: number): Observable<SpaceType> {
    return this.getEntityFromServer(
      `${this._apiBaseUrl}/spaceType/${spaceTypeId}`
    );
  }

  getEntities(entityFilter: IEntityFilter): Observable<SpaceType[]> {
    const searchKeyword = entityFilter ? entityFilter.searchKeyword : '';
    return this.getEntitiesFromServer(
      `${this._apiBaseUrl}/spaceType?spaceTypeName=${searchKeyword}`
    );
  }

  deleteMultipleItems(spaceTypes: SpaceType[]): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/spaceType/deleteMultipleSpaceTypes`,
      spaceTypes
    );
  }

  deleteItem(spaceTypeId: number): Observable<void> {
    return this.deleteItemOnServer(
      `${this._apiBaseUrl}/spaceType/${spaceTypeId}`
    );
  }

  saveNewEntity(spaceTypeForCreate: IDtoProcess): Observable<void> {
    return this.saveNewEntityToServer(
      `${this._apiBaseUrl}/spaceType/create`,
      spaceTypeForCreate.dtoEntity
    );
  }

  updateEntity(spaceTypeForUpdate: IDtoProcess): Observable<void> {
    return this.updateEntityToServer(
      `${this._apiBaseUrl}/spaceType/${spaceTypeForUpdate.id}`,
      spaceTypeForUpdate.dtoEntity
    );
  }

  reloadListFlag() {
    this._loadSpaceTypesFlag.next();
  }

  get loadSpaceTypesFlag(): BehaviorSubject<void> {
    return this._loadSpaceTypesFlag;
  }
}
