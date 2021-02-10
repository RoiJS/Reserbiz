import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { Routes } from '@angular/router';

import { NativeScriptRouterModule } from '@nativescript/angular';

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
  schemas: [NO_ERRORS_SCHEMA]
})
export class ProfileRoutingModule {}
