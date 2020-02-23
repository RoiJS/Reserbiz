import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Page } from 'tns-core-modules/ui/page/page';

import { RouterExtensions } from 'nativescript-angular/router';

@Component({
  selector: 'ns-profile-tabs',
  templateUrl: './profile-tabs.component.html',
  styleUrls: ['./profile-tabs.component.scss']
})
export class ProfileTabsComponent implements OnInit {
  private _isLoading: boolean;

  constructor(
    private page: Page,
    private router: RouterExtensions,
    private active: ActivatedRoute
  ) {
    this._isLoading = false;
  }

  ngOnInit() {
    this.page.actionBarHidden = true;
    this.loadTabRoutes();
  }

  loadTabRoutes() {
    setTimeout(() => {
      this.router.navigate(
        [
          {
            outlets: {
              personalInfo: ['personalInfo'],
              accountInfo: ['accountInfo']
            }
          }
        ],
        { relativeTo: this.active }
      );
    }, 10);
  }

  get isLoading(): boolean {
    return this._isLoading;
  }
}
