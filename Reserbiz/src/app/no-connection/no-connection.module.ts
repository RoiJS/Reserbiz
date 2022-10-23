import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptCommonModule } from '@nativescript/angular';

import { SharedModule } from '../shared/shared.module';
import { NoConnectionRoutingModule } from './no-connection-routing.module';

import { NoConnectionComponent } from './no-connection.component';

@NgModule({
  imports: [
    NativeScriptCommonModule,
    NoConnectionRoutingModule,
    SharedModule
  ],
  declarations: [NoConnectionComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class NoConnectionModule { }
