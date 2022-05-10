import { Component, OnInit, Input } from '@angular/core';

import { RouterExtensions } from '@nativescript/angular';

import { Tenant } from '../../../_models/tenant.model';

@Component({
  selector: 'ns-tenant-details-panel',
  templateUrl: './tenant-details-panel.component.html',
  styleUrls: ['./tenant-details-panel.component.scss'],
})
export class TenantDetailsPanelComponent implements OnInit {
  @Input() currentTenant: Tenant;
  constructor(private router: RouterExtensions) {}

  ngOnInit() {}

  onNavigateToEditPage() {
    this.router.navigate([`/tenants/${this.currentTenant.id}/edit`], {
      transition: {
        name: 'slideLeft',
      },
    });
  }
}
