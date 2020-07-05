import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { ProfileTabsComponent } from './profile-tabs/profile-tabs.component';
import { ProfileAccountInfoComponent } from './profile-account-info/profile-account-info.component';
import { ProfilePersonalInfoComponent } from './profile-personal-info/profile-personal-info.component';
import { ProfileInformationComponent } from './profile-information/profile-information.component';

const routes: Routes = [
  {
    path: '',
    component: ProfileInformationComponent,
  },
  {
    path: 'personalInfo',
    loadChildren: () =>
      import('./profile-personal-info/profile-personal-info.module').then(
        (m) => m.ProfilePersonalInfoModule
      ),
  },
  {
    path: 'accountInfo',
    loadChildren: () =>
      import('./profile-account-info/profile-account-info.module').then(
        (m) => m.ProfilePersonalInfoModule
      ),
  },
];

@NgModule({
  imports: [NativeScriptRouterModule.forChild(routes)],
  exports: [NativeScriptRouterModule],
})
export class ProfileRoutingModule {}
