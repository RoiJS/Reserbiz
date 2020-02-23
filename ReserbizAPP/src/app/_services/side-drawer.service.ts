import { Injectable } from '@angular/core';
import { MainMenu } from '../_models/main-menu.model';

@Injectable({
  providedIn: 'root'
})
export class SideDrawerService {

  mainMenu: MainMenu[] = [
    {
      text: 'Dashboard',
      icon: String.fromCharCode(0xf51b),
      url: '/dashboard',
      hasSeparator: false
    },
    {
      text: 'Tenants',
      icon: String.fromCharCode(0xf0c0),
      url: '/tenants',
      hasSeparator: false
    },
    {
      text: 'Contracts',
      icon: String.fromCharCode(0xf573),
      url: '/contracts',
      hasSeparator: false
    },
    {
      text: 'Terms',
      icon: String.fromCharCode(0xf570),
      url: '/terms',
      hasSeparator: false
    },
    {
      text: 'Space Types',
      icon: String.fromCharCode(0xf015),
      url: '/space-types',
      hasSeparator: false
    },
    {
      text: 'Profile',
      icon: String.fromCharCode(0xf2bd),
      url: '/profile',
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
