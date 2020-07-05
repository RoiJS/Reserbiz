import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../shared/shared.module';

import { ProfileAccountInfoComponent } from './profile-account-info.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ProfileAccountInfoComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [ProfileAccountInfoComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ProfilePersonalInfoModule {}
