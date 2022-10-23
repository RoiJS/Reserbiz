import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { NativeScriptUIDataFormModule } from 'nativescript-ui-dataform/angular';

import { TenantEditDetailsComponent } from './tenant-edit-details.component';

import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantEditDetailsComponent,
      },
    ]),
    NativeScriptUIDataFormModule,
    SharedModule,
  ],
  declarations: [TenantEditDetailsComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantEditDetailsModule {}
