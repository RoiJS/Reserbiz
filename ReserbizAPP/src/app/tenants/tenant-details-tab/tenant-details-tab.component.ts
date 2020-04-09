import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { RouterExtensions, PageRoute } from 'nativescript-angular/router';
import { Page } from 'tns-core-modules/ui/page/page';

@Component({
  selector: 'ns-tenant-details',
  templateUrl: './tenant-details-tab.component.html',
  styleUrls: ['./tenant-details-tab.component.scss'],
})
export class TenantDetailsTabComponent implements OnInit {
  private _isLoading: boolean;

  constructor(
    private page: Page,
    private pageRoute: PageRoute,
    private router: RouterExtensions,
    private active: ActivatedRoute
  ) {
    this._isLoading = false;
  }

  ngOnInit() {
    this.pageRoute.activatedRoute.subscribe((activatedRoute) => {
      activatedRoute.paramMap.subscribe((paramMap) => {
        const tenantId = +paramMap.get('tenantId');
        this.page.actionBarHidden = true;
        this.loadTabRoutes(tenantId);
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

  get isLoading(): boolean {
    return this._isLoading;
  }
}
