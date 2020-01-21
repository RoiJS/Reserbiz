import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { NativeScriptFormsModule } from 'nativescript-angular/forms';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { AuthComponent } from './auth.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptCommonModule,
    NativeScriptFormsModule,
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: AuthComponent
      }
    ]),
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [AuthComponent]
})
export class AuthModule {}
