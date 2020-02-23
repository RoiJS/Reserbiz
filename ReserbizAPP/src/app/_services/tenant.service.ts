import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Tenant } from '../_models/tenant.model';
import { environment } from '@src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TenantService {
  constructor(private http: HttpClient) {}

  getTenants(): Observable<Tenant[]> {
      return this.http.get<Tenant[]>(`${environment.reserbizAPIEndPoint}/tenant`);
  }
}
