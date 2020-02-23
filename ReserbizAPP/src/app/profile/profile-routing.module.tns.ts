import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { ProfileTabsComponent } from './profile-tabs/profile-tabs.component';
import { ProfileAccountInfoComponent } from './profile-account-info/profile-account-info.component';
import { ProfilePersonalInfoComponent } from './profile-personal-info/profile-personal-info.component';

const routes: Routes = [
  {
    path: 'tabs',
    component: ProfileTabsComponent,
    children: [
      {
        path: 'accountInfo',
        component: ProfileAccountInfoComponent,
        outlet: 'accountInfo'
      },
      {
        path: 'personalInfo',
        component: ProfilePersonalInfoComponent,
        outlet: 'personalInfo'
      }
    ]
  },
  {
    path: '',
    redirectTo: '/profile/tabs',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [NativeScriptRouterModule.forChild(routes)],
  exports: [NativeScriptRouterModule]
})
export class ProfileRoutingModule {}
