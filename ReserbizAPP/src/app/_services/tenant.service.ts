import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';

import { Tenant } from '../_models/tenant.model';
import { environment } from '@src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TenantService {
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
    return this.http
      .get<Tenant>(`${environment.reserbizAPIEndPoint}/tenant/${tenantId}`)
      .pipe(
        map((tenant: Tenant) => {
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
        })
      );
  }
}
