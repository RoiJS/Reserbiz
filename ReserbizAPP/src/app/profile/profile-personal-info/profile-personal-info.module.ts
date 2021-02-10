import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { ProfilePersonalInfoComponent } from './profile-personal-info.component';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: ProfilePersonalInfoComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [ProfilePersonalInfoComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class ProfilePersonalInfoModule {}
