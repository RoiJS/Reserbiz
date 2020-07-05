import { Injectable } from '@angular/core';
import { MainMenu } from '../_models/main-menu.model';

@Injectable({
  providedIn: 'root',
})
export class SideDrawerService {
  mainMenu: MainMenu[];

  constructor() {
    this.mainMenu = [
      {
        text: 'MAIN_MENU.DASHBOARD',
        icon: String.fromCharCode(0xf51b),
        url: '/dashboard',
        hasSeparator: false,
      },
      {
        text: 'MAIN_MENU.TENANTS',
        icon: String.fromCharCode(0xf0c0),
        url: '/tenants',
        hasSeparator: false,
      },
      {
        text: 'MAIN_MENU.SPACE_TYPES',
        icon: String.fromCharCode(0xf015),
        url: '/space-types',
        hasSeparator: false,
      },
      {
        text: 'MAIN_MENU.TERMS',
        icon: String.fromCharCode(0xf570),
        url: '/terms',
        hasSeparator: false,
      },
      {
        text: 'MAIN_MENU.CONTRACTS',
        icon: String.fromCharCode(0xf573),
        url: '/contracts',
        hasSeparator: false,
      },

      {
        text: 'MAIN_MENU.PROFILE',
        icon: String.fromCharCode(0xf2bd),
        url: '/profile',
        hasSeparator: false,
      },
      {
        text: 'MAIN_MENU.SETTINGS',
        icon: String.fromCharCode(0xf013),
        url: '/settings',
        hasSeparator: true,
      },
    ];
  }
}
