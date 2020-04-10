import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { isAndroid } from 'tns-core-modules/platform';
import { Page } from 'tns-core-modules/ui/page/page';
import { RouterExtensions } from 'nativescript-angular/router';

import { UIService } from '../../../_services/ui.service';

declare var android: any;

@Component({
  selector: 'ns-action-bar',
  templateUrl: './action-bar.component.html',
  styleUrls: ['./action-bar.component.scss'],
})
export class ActionBarComponent implements OnInit {
  @Input() title = '';
  @Input() showBackButton = true;
  @Input() hasMenu = true;

  @Output() onNavigateBack = new EventEmitter();

  constructor(
    private page: Page,
    private router: RouterExtensions,
    private uiService: UIService
  ) {}

  ngOnInit() {}

  get android() {
    return isAndroid;
  }

  get canGoBack() {
    return this.showBackButton;
  }

  onGoBack() {
    if (this.router.canGoBack()) {
      this.router.back();
    } else {
      this.onNavigateBack.emit();
    }
  }

  onLoadedActionBar() {
    if (isAndroid) {
      const androidToolbar = this.page.actionBar.nativeView;
      const backButton = androidToolbar.getNavigationIcon();

      let color = '#171717';
      if (this.hasMenu) {
        color = '#ffffff';
      }

      if (backButton) {
        backButton.setColorFilter(
          android.graphics.Color.parseColor(color),
          (<any>android.graphics).PorterDuff.Mode.SRC_ATOP
        );
      }
    }
  }

  onToggleMenu() {
    this.uiService.toggleDrawer();
  }
}
