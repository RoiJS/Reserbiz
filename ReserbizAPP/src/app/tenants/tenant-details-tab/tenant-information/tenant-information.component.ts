import { Component, OnInit } from '@angular/core';
import { PageRoute, RouterExtensions } from 'nativescript-angular/router';
import { Page } from 'tns-core-modules/ui/page/page';

import { TenantService } from '@src/app/_services/tenant.service';
import { Tenant } from '@src/app/_models/tenant.model';

@Component({
  selector: 'ns-tenant-information',
  templateUrl: './tenant-information.component.html',
  styleUrls: ['./tenant-information.component.scss'],
})
export class TenantInformationComponent implements OnInit {
  private _actionBarTitle = '';
  private _currentTenant: Tenant;

  constructor(
    private pageRoute: PageRoute,
    private page: Page,
    private router: RouterExtensions,
    private tenantService: TenantService
  ) {}

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        this.page.actionBarHidden = true;
        const tenantId = +paramMap.get('tenantId');
        this.tenantService.getTenant(tenantId).subscribe((tenant: Tenant) => {
          this._currentTenant = tenant;
        });
      });
    });
  }

  onGoBack() {
    this.router.navigate(['/tenants'], {
      transition: {
        name: 'slideRight',
      },
    });
  }

  get ActionBarTitle(): string {
    return this._actionBarTitle;
  }
}
