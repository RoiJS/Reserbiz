import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { NativeScriptUIDataFormModule } from 'nativescript-ui-dataform/angular';

import { SharedModule } from '../shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module';

import { ProfileInformationComponent } from './profile-information/profile-information.component';
import { ProfileGeneralInformationComponent } from './profile-information/profile-general-information/profile-general-information.component';
import { ProfileAccountInformationComponent } from './profile-information/profile-account-information/profile-account-information.component';

@NgModule({
  imports: [
    NativeScriptUIDataFormModule,
    NativeScriptCommonModule,
    SharedModule,
    ProfileRoutingModule,
  ],
  declarations: [
    ProfileInformationComponent,
    ProfileGeneralInformationComponent,
    ProfileAccountInformationComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ProfileModule {}
