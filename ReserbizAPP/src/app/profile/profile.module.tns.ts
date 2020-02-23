import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { NativeScriptUIDataFormModule } from 'nativescript-ui-dataform/angular';

import { SharedModule } from '../shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module.tns';

import { ProfileTabsComponent } from './profile-tabs/profile-tabs.component';
import { ProfilePersonalInfoComponent } from './profile-personal-info/profile-personal-info.component';
import { ProfileAccountInfoComponent } from './profile-account-info/profile-account-info.component';

@NgModule({
  imports: [
    NativeScriptUIDataFormModule,
    NativeScriptCommonModule,
    SharedModule,
    ProfileRoutingModule
  ],
  declarations: [
    ProfileTabsComponent,
    ProfilePersonalInfoComponent,
    ProfileAccountInfoComponent
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class ProfileModule {}
