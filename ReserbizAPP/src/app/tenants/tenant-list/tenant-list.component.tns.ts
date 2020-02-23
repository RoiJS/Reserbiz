import { Component, OnInit } from '@angular/core';

import { ObservableArray } from 'tns-core-modules/data/observable-array';

import { TenantService } from '@src/app/_services/tenant.service';
import { Tenant } from '@src/app/_models/tenant.model';

@Component({
  selector: 'ns-tenant-list',
  templateUrl: './tenant-list.component.html',
  styleUrls: ['./tenant-list.component.scss']
})
export class TenantListComponent implements OnInit {
  private _tenants: ObservableArray<Tenant>;

  constructor(private tenantService: TenantService) {}

  ngOnInit() {
    this.tenantService.getTenants().subscribe(tenants => {
      this._tenants = new ObservableArray<Tenant>(tenants);
    });
  }

  get tenants(): ObservableArray<Tenant> {
    return this._tenants;
  }
}
