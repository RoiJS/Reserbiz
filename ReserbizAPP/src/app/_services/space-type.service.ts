import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

import { BaseService } from './base.service';
import { SpaceTypeMapper } from '../_helpers/space-type-mapper.helper';
import { SpaceType } from '../_models/space-type.model';
import { SpaceTypeOption } from '../_models/space-type-option.model';
import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IEntityFilter } from '../_interfaces/ientity-filter.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';

@Injectable({ providedIn: 'root' })
export class SpaceTypeService extends BaseService<SpaceType>
  implements IBaseService<SpaceType> {
  private _loadSpaceTypesFlag = new BehaviorSubject<void>(null);
  constructor(public http: HttpClient) {
    super(new SpaceTypeMapper(), http);
  }

  getSpaceType(spaceTypeId: number): Observable<SpaceType> {
    return this.getEntityFromServer(
      `${this._apiBaseUrl}/spaceType/${spaceTypeId}`
    );
  }

  getSpaceTypesAsOptions(
    translateService: TranslateService
  ): Observable<SpaceTypeOption[]> {
    return this.http
      .get<SpaceTypeOption[]>(
        `${this._apiBaseUrl}/spaceType/getSpaceTypeAsOptions`
      )
      .pipe(
        map((stos: SpaceTypeOption[]) => {
          return stos.map((st: SpaceTypeOption) => {
            const spaceTypeOption = new SpaceTypeOption();
            spaceTypeOption.id = st.id;
            spaceTypeOption.name = st.name;
            spaceTypeOption.rate = st.rate;
            spaceTypeOption.isDelete = st.isDelete;
            spaceTypeOption.isActive = st.isActive;
            spaceTypeOption.canBeSelected = st.canBeSelected;
            spaceTypeOption.inactiveText = translateService.instant(
              'GENERAL_TEXTS.INACTIVE'
            );
            return spaceTypeOption;
          });
        })
      );
  }

  getEntities(entityFilter: IEntityFilter): Observable<SpaceType[]> {
    const searchKeyword = entityFilter.searchKeyword || '';
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

  async checkAvailability(spaceTypeId: number): Promise<boolean> {
    return this.http
      .get<boolean>(
        `${this._apiBaseUrl}/spaceType/checkSpaceTypeAvailability/${spaceTypeId}`
      )
      .toPromise();
  }

  async validateSpaceTypeProposedNewAvailableSlot(
    spaceTypeId: number,
    proposedNewValue: number
  ): Promise<boolean> {
    return this.http
      .get<boolean>(
        `${this._apiBaseUrl}/spaceType/validateSpaceTypeProposedNewAvailableSlot/${spaceTypeId}/${proposedNewValue}`
      )
      .toPromise();
  }

  reloadListFlag() {
    this._loadSpaceTypesFlag.next();
  }

  get loadSpaceTypesFlag(): BehaviorSubject<void> {
    return this._loadSpaceTypesFlag;
  }
}
