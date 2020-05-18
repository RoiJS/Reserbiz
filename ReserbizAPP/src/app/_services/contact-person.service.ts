import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';

import { environment } from '@src/environments/environment';

import { BaseService } from './base.service';
import { ContactPersonMapper } from '../_helpers/contact-person-mapper.helper';
import { ContactPerson } from '../_models/contact-person.model';
import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IEntityFilter } from '../_interfaces/ientity-filter.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';

@Injectable({
  providedIn: 'root',
})
export class ContactPersonService extends BaseService<ContactPerson>
  implements IBaseService<ContactPerson> {
  private _apiBaseUrl = environment.reserbizAPIEndPoint;
  private _updateContactPersonListFlag = new BehaviorSubject<void>(null);

  constructor(public http: HttpClient) {
    super(new ContactPersonMapper(), http);
  }

  getContactPerson(contactPersonId: number): Observable<ContactPerson> {
    return this.getEntityFromServer(
      `${this._apiBaseUrl}/ContactPerson/${contactPersonId}`
    );
  }

  getEntities(entityFilter: IEntityFilter): Observable<ContactPerson[]> {
    return this.getEntitiesFromServer(
      `${this._apiBaseUrl}/ContactPerson/getAllContactPersonsPerTenant/${entityFilter.parentId}`
    );
  }

  deleteMultipleItems(contactPersons: ContactPerson[]): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/contactPerson/deleteMultipleContactPersons`,
      contactPersons
    );
  }

  deleteItem(contactPersonId: number): Observable<void> {
    return this.deleteItemOnServer(
      `${this._apiBaseUrl}/contactPerson/${contactPersonId}`
    );
  }

  saveNewEntity(contactPersonForCreate: IDtoProcess): Observable<void> {
    return this.saveNewEntityToServer(
      `${this._apiBaseUrl}/contactPerson/create?tenantId=${contactPersonForCreate.id}`,
      contactPersonForCreate.dtoEntity
    );
  }

  updateEntity(contactPersonUpdateDto: IDtoProcess): Observable<void> {
    return this.updateEntityToServer(
      `${this._apiBaseUrl}/contactPerson/${contactPersonUpdateDto.id}`,
      contactPersonUpdateDto.dtoEntity
    );
  }

  reloadListFlag() {
    this._updateContactPersonListFlag.next();
  }

  get updateContactPersonListFlag(): BehaviorSubject<void> {
    return this._updateContactPersonListFlag;
  }
}
