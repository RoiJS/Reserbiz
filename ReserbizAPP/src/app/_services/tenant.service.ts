import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';

import { Tenant } from '../_models/tenant.model';
import { environment } from '@src/environments/environment';
import { map } from 'rxjs/operators';
import { ContactPerson } from '../_models/contact-person.model';

@Injectable({
  providedIn: 'root',
})
export class TenantService {
  private _apiBaseUrl = environment.reserbizAPIEndPoint;

  constructor(private http: HttpClient) {}

  getTenants(): Observable<Tenant[]> {
    return this.http
      .get<Tenant[]>(`${environment.reserbizAPIEndPoint}/tenant`)
      .pipe(
        map((tenants: Tenant[]) => {
          return tenants.map((tenant) => {
            return new Tenant(
              tenant.id,
              tenant.firstName,
              tenant.middleName,
              tenant.lastName,
              tenant.gender,
              tenant.address,
              tenant.contactNumber,
              tenant.emailAddress,
              tenant.photoUrl,
              tenant.isActive
            );
          });
        })
      );
  }

  getTenant(tenantId: number): Observable<Tenant> {
    return this.http.get<Tenant>(`${this._apiBaseUrl}/tenant/${tenantId}`).pipe(
      map((t: Tenant) => {
        const tenant = new Tenant(
          t.id,
          t.firstName,
          t.middleName,
          t.lastName,
          t.gender,
          t.address,
          t.contactNumber,
          t.emailAddress,
          t.photoUrl,
          t.isActive
        );

        tenant.contactPersons = t.contactPersons.map((c: ContactPerson) => {
          return new ContactPerson(
            c.id,
            c.firstName,
            c.middlename,
            c.lastName,
            c.gender,
            c.contactNumber,
            c.tenanId
          );
        });

        return tenant;
      })
    );
  }

  deleteMultipleTenants(tenants: Tenant[]): Observable<void> {
    const tenantIds = tenants.map((tenant) => tenant.id);

    return this.http.post<void>(
      `${this._apiBaseUrl}/tenant/deleteMultipleTenants`,
      tenantIds
    );
  }

  deleteTenant(tenantId: number): Observable<void> {
    return this.http.delete<void>(
      `${this._apiBaseUrl}/tenant/deleteTenant?tenantId=${tenantId}`
    );
  }

  setTenantStatus(tenantId: number, status: boolean): Observable<void> {
    return this.http.put<void>(
      `${this._apiBaseUrl}/tenant/setStatus/${tenantId}/${status}`,
      null
    );
  }
}
