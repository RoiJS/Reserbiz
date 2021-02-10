import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@src/environments/environment';

import { IEntity } from '../_interfaces/ientity.interface';
import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';
import { IBaseDto } from '../_interfaces/ibase-dto.interface';
import { IEntityPaginationList } from '../_interfaces/ientity-pagination-list.interface';
import { EntityPaginationList } from '../_models/entity-pagination-list.model';

Injectable({ providedIn: 'root' });
export class BaseService<TEntity extends IEntity> {
  protected _apiBaseUrl = environment.reserbizAPIEndPoint;

  constructor(
    public mapper: IBaseEntityMapper<IEntity>,
    public http: HttpClient
  ) {}

  getPaginatedEntitiesFromServer(
    url: string,
    params?: any
  ): Observable<IEntityPaginationList> {
    return this.http
      .get<IEntityPaginationList>(url, { params })
      .pipe(
        map((data: IEntityPaginationList) => {
          return this.mapPaginatedEntityData(data);
        })
      );
  }

  getEntitiesFromServer(url: string): Observable<TEntity[]> {
    return this.http.get<TEntity[]>(url).pipe(
      map((data: TEntity[]) => {
        return data.map((d: TEntity) => {
          return <TEntity>this.mapper.mapEntity(d);
        });
      })
    );
  }

  getEntityFromServer(url: string): Observable<TEntity> {
    return this.http.get<TEntity>(url).pipe(
      map((data: TEntity) => {
        return <TEntity>this.mapper.mapEntity(data);
      })
    );
  }

  deleteMultipleItemsOnServer(
    url: string,
    entities: TEntity[]
  ): Observable<void> {
    const entitiesId = entities.map((e) => e.id);
    return this.http.post<void>(url, entitiesId);
  }

  deleteItemOnServer(url: string): Observable<void> {
    return this.http.delete<void>(url);
  }

  setEntityStatusOnServer(url: string): Observable<void> {
    return this.http.put<void>(url, null);
  }

  setMultipleEntityStatusOnServer(
    url: string,
    entities: TEntity[],
    status: boolean
  ): Observable<void> {
    const entityIds = entities.map((e) => e.id);

    const params = JSON.stringify({ entityIds, status });
    return this.http.post<void>(url, entityIds);
  }

  saveNewEntityToServer(url: string, dtoToCreate: IBaseDto): Observable<void> {
    return this.http.post<void>(url, dtoToCreate);
  }

  updateEntityToServer(url: string, dtoForUpdate: IBaseDto): Observable<void> {
    return this.http.put<void>(url, dtoForUpdate);
  }

  mapPaginatedEntityData(data: IEntityPaginationList): EntityPaginationList {
    const entityPaginationList = new EntityPaginationList();

    entityPaginationList.totalItems = data.totalItems;
    entityPaginationList.page = data.page;
    entityPaginationList.numberOfItemsPerPage = data.numberOfItemsPerPage;
    const items = data.items.map((d: TEntity) => {
      return <TEntity>this.mapper.mapEntity(d);
    });

    entityPaginationList.items = items;

    return entityPaginationList;
  }

  parseRequestParams(data: any): any {
    return Object.keys(data)
      .filter((e) => Boolean(data[e]))
      .reduce((o, e) => {
        o[e] = data[e];
        return o;
      }, {});
  }
}
