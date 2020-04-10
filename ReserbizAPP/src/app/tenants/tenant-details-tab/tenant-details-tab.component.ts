import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { RouterExtensions, PageRoute } from 'nativescript-angular/router';
import { Page } from 'tns-core-modules/ui/page/page';

import { TenantService } from '@src/app/_services/tenant.service';
import { Tenant } from '@src/app/_models/tenant.model';

@Component({
  selector: 'ns-tenant-details',
  templateUrl: './tenant-details-tab.component.html',
  styleUrls: ['./tenant-details-tab.component.scss'],
})
export class TenantDetailsTabComponent implements OnInit {
  private _isLoading: boolean;
  private _actionBarTitle = '';

  constructor(
    private page: Page,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private active: ActivatedRoute,
    private tenantService: TenantService
  ) {
    this._isLoading = false;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const tenantId = +paramMap.get('tenantId');
        this.tenantService.getTenant(tenantId).subscribe((tenant: Tenant) => {
          this._actionBarTitle = tenant.fullName;
          //this.loadTabRoutes(tenantId);
        });
      });
    });
  }

  loadTabRoutes(tenantId: number) {
    setTimeout(() => {
      this.router.navigate(
        [
          {
            outlets: {
              tenantDetails: [`tenantDetails`, tenantId],
              tenantContractList: ['tenantContractList'],
            },
          },
        ],
        { relativeTo: this.active }
      );
    }, 10);
  }

  get ActionBarTitle(): string {
    return this._actionBarTitle;
  }

  get isLoading(): boolean {
    return this._isLoading;
  }
}
