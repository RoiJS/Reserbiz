import { Injectable } from '@angular/core';
import { MainMenu } from '../_models/main-menu.model';

@Injectable({
  providedIn: 'root'
})
export class SideDrawerService {

  mainMenu: MainMenu[] = [
    {
      text: 'Dashboard',
      icon: String.fromCharCode(0xf015),
      url: '/dashboard',
      hasSeparator: false
    },
    {
      text: 'Tenants',
      icon: String.fromCharCode(0xf1ea),
      url: '/tenants',
      hasSeparator: false
    },
    {
      text: 'Contracts',
      icon: String.fromCharCode(0xf002),
      url: '/contracts',
      hasSeparator: false
    },
    {
      text: 'Terms',
      icon: String.fromCharCode(0xf005),
      url: '/terms',
      hasSeparator: false
    },
    {
      text: 'Space Types',
      icon: String.fromCharCode(0xf013),
      url: '/space-types',
      hasSeparator: false
    },
    {
      text: 'Settings',
      icon: String.fromCharCode(0xf013),
      url: '/settings',
      hasSeparator: true
    }
  ];

  constructor() {}
}
