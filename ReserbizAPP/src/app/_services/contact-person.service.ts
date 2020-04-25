import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ContactPerson } from '../_models/contact-person.model';
import { environment } from '@src/environments/environment';
import { ContactPersonCreateDto } from '../_dtos/contact-person-create.dto';
import { ContactPersonUpdateDto } from '../_dtos/contact-person-update.dto';

@Injectable({
  providedIn: 'root',
})
export class ContactPersonService {
  private _apiBaseUrl = environment.reserbizAPIEndPoint;
  constructor(private http: HttpClient) {}

  getContactPersons(tenantId: number): Observable<ContactPerson[]> {
    return this.http
      .get<ContactPerson[]>(
        `${this._apiBaseUrl}/ContactPerson/getAllContactPersonsPerTenant/${tenantId}`
      )
      .pipe(
        map((contactPersons: ContactPerson[]) => {
          return contactPersons.map((cp: ContactPerson) => {
            const contactPerson = new ContactPerson();

            contactPerson.id = cp.id;
            contactPerson.firstName = cp.firstName;
            contactPerson.middleName = cp.middleName;
            contactPerson.lastName = cp.lastName;
            contactPerson.gender = cp.gender;
            contactPerson.contactNumber = cp.contactNumber;
            contactPerson.tenantId = cp.tenantId;

            return contactPerson;
          });
        })
      );
  }

  getContactPerson(contactPersonId: number): Observable<ContactPerson> {
    return this.http
      .get<ContactPerson>(
        `${this._apiBaseUrl}/ContactPerson/${contactPersonId}`
      )
      .pipe(
        map((cp: ContactPerson) => {
          const contactPerson = new ContactPerson();
          contactPerson.id = cp.id;
          contactPerson.firstName = cp.firstName;
          contactPerson.middleName = cp.middleName;
          contactPerson.lastName = cp.lastName;
          contactPerson.gender = cp.gender;
          contactPerson.contactNumber = cp.contactNumber;
          contactPerson.tenantId = cp.tenantId;
          return contactPerson;
        })
      );
  }

  deleteMultipleContactPersons(
    contactPersons: ContactPerson[]
  ): Observable<void> {
    const contactPersonIds = contactPersons.map(
      (contactPerson) => contactPerson.id
    );

    return this.http.post<void>(
      `${this._apiBaseUrl}/contactPerson/deleteMultipleContactPersons`,
      contactPersonIds
    );
  }

  deleteContactPerson(contactPersonId: number): Observable<void> {
    return this.http.delete<void>(
      `${this._apiBaseUrl}/contactPerson/${contactPersonId}`
    );
  }

  createContactPerson(
    tenantId: number,
    contactPersonForCreate: ContactPersonCreateDto
  ): Observable<ContactPersonCreateDto> {
    return this.http.post<ContactPersonCreateDto>(
      `${this._apiBaseUrl}/contactPerson/create?tenantId=${tenantId}`,
      contactPersonForCreate
    );
  }

  updateContactPerson(
    tenantId: number,
    contactPersonUpdateDto: ContactPersonUpdateDto
  ): Observable<void> {
    return this.http.put<void>(
      `${this._apiBaseUrl}/contactPerson/${tenantId}`,
      contactPersonUpdateDto
    );
  }
}
