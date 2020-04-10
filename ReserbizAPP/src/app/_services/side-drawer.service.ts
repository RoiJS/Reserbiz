import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { MainMenu } from '../_models/main-menu.model';

@Injectable({
  providedIn: 'root',
})
export class SideDrawerService {
   mainMenu: MainMenu[];

  constructor(private translateService: TranslateService) {
    this.mainMenu = [
      {
        text: this.translateService.instant('MAIN_MENU.DASHBOARD'),
        icon: String.fromCharCode(0xf51b),
        url: '/dashboard',
        hasSeparator: false,
      },
      {
        text: this.translateService.instant('MAIN_MENU.TENANTS'),
        icon: String.fromCharCode(0xf0c0),
        url: '/tenants',
        hasSeparator: false,
      },
      {
        text: this.translateService.instant('MAIN_MENU.CONTRACTS'),
        icon: String.fromCharCode(0xf573),
        url: '/contracts',
        hasSeparator: false,
      },
      {
        text: this.translateService.instant('MAIN_MENU.TERMS'),
        icon: String.fromCharCode(0xf570),
        url: '/terms',
        hasSeparator: false,
      },
      {
        text: this.translateService.instant('MAIN_MENU.SPACE_TYPES'),
        icon: String.fromCharCode(0xf015),
        url: '/space-types',
        hasSeparator: false,
      },
      {
        text: this.translateService.instant('MAIN_MENU.PROFILE'),
        icon: String.fromCharCode(0xf2bd),
        url: '/profile',
        hasSeparator: false,
      },
      {
        text: this.translateService.instant('MAIN_MENU.SETTINGS'),
        icon: String.fromCharCode(0xf013),
        url: '/settings',
        hasSeparator: true,
      },
    ];
  }
}
