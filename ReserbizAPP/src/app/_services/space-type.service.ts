import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@src/environments/environment';

import { SpaceType } from '../_models/space-type.model';
import { SpaceTypeDto } from '../_dtos/space-type.dto';

@Injectable({ providedIn: 'root' })
export class SpaceTypeService {
  private _apiBaseUrl = environment.reserbizAPIEndPoint;
  private _loadSpaceTypesFlag = new BehaviorSubject<void>(null);

  constructor(private http: HttpClient) {}

  getSpaceTypes(name: string): Observable<SpaceType[]> {
    return this.http
      .get<SpaceType[]>(`${this._apiBaseUrl}/spaceType?spaceTypeName=${name}`)
      .pipe(
        map((spaceTypes: SpaceType[]) => {
          return spaceTypes.map((st: SpaceType) => {
            const spaceType = new SpaceType();
            spaceType.id = st.id;
            spaceType.name = st.name;
            spaceType.description = st.description;
            spaceType.rate = st.rate;
            spaceType.availableSlot = st.availableSlot;
            spaceType.isActive = st.isActive;
            spaceType.isDeletable = st.isDeletable;
            return spaceType;
          });
        })
      );
  }

  getSpaceType(id: number): Observable<SpaceType> {
    return this.http.get<SpaceType>(`${this._apiBaseUrl}/spaceType/${id}`).pipe(
      map((st: SpaceType) => {
        const spaceType = new SpaceType();
        spaceType.id = st.id;
        spaceType.name = st.name;
        spaceType.description = st.description;
        spaceType.rate = st.rate;
        spaceType.availableSlot = st.availableSlot;
        spaceType.isActive = st.isActive;
        spaceType.isDeletable = st.isDeletable;
        return spaceType;
      })
    );
  }

  deleteMultipleSpaceTypes(spaceTypes: SpaceType[]): Observable<void> {
    const spaceTypeIds = spaceTypes.map((spaceType) => spaceType.id);

    return this.http.post<void>(
      `${this._apiBaseUrl}/spaceType/deleteMultipleSpaceTypes`,
      spaceTypeIds
    );
  }

  deleteSpaceType(tenantId: number): Observable<void> {
    return this.http.delete<void>(`${this._apiBaseUrl}/spaceType/${tenantId}`);
  }

  saveNewSpaceType(spaceTypeForCreate: SpaceTypeDto): Observable<void> {
    return this.http.post<void>(
      `${this._apiBaseUrl}/spaceType/create`,
      spaceTypeForCreate
    );
  }

  updateSpaceType(
    id: number,
    spaceTypeForUpdate: SpaceTypeDto
  ): Observable<void> {
    return this.http.put<void>(
      `${this._apiBaseUrl}/spaceType/${id}`,
      spaceTypeForUpdate
    );
  }

  get loadSpaceTypesFlag(): BehaviorSubject<void> {
    return this._loadSpaceTypesFlag;
  }
}
