import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { IEntity } from '../_interfaces/ientity.interface';
import { map } from 'rxjs/operators';
import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';
import { IBaseDTO } from '../_interfaces/ibase-dto.interface';

Injectable({ providedIn: 'root' });
export class BaseService<TEntity extends IEntity> {
  constructor(
    public mapper: IBaseEntityMapper<IEntity>,
    public http: HttpClient
  ) {}

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
    const entitiesId = entities.map((tenant) => tenant.id);
    return this.http.post<void>(url, entitiesId);
  }

  deleteItemOnServer(url: string): Observable<void> {
    return this.http.delete<void>(url);
  }

  setEntityStatusOnServer(url: string): Observable<void> {
    return this.http.put<void>(url, null);
  }

  saveNewEntityToServer(url: string, dtoToCreate: IBaseDTO): Observable<void> {
    return this.http.post<void>(url, dtoToCreate);
  }

  updateEntityToServer(url: string, dtoForUpdate: IBaseDTO): Observable<void> {
    return this.http.put<void>(url, dtoForUpdate);
  }
}
