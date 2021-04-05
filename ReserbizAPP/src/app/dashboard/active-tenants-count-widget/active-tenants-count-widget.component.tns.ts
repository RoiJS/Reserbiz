import { Component, OnInit } from '@angular/core';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';

import { TenantService } from '@src/app/_services/tenant.service';

@Component({
  selector: 'ns-active-tenants-count-widget',
  templateUrl: './active-tenants-count-widget.component.html',
  styleUrls: ['./active-tenants-count-widget.component.scss'],
})
export class ActiveTenantsCountWidgetComponent
  extends BaseWidgetComponent
  implements OnInit {
  constructor(private tenantService: TenantService) {
    super();
  }

  ngOnInit() {
    this._isBusy = true;
    setTimeout(() => {
      (async () => {
        this._entityCount = await this.tenantService.getActiveTenantsCount();
        this._isBusy = false;
      })();
    }, 2000);
  }
}
