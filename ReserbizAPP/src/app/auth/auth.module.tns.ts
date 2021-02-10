import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import {
  NativeScriptCommonModule,
  NativeScriptFormsModule,
  NativeScriptRouterModule,
} from '@nativescript/angular';

import { AuthComponent } from './auth.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptCommonModule,
    NativeScriptFormsModule,
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: AuthComponent,
      },
    ]),
    ReactiveFormsModule,
    SharedModule,
  ],
  declarations: [AuthComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class AuthModule {}
