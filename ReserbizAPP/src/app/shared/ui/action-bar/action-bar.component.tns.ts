import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { isAndroid, Page } from '@nativescript/core';
import { RouterExtensions } from '@nativescript/angular';

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
  @Input() overrideBackAction = false;

  @Output() onNavigationBack = new EventEmitter<void>();

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
    if (!this.overrideBackAction) {
      if (this.router.canGoBack()) {
        this.uiService.hideKeyboard();
        this.router.back();
      }
    } else {
      this.onNavigationBack.emit();
      this.uiService.hideKeyboard();
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
