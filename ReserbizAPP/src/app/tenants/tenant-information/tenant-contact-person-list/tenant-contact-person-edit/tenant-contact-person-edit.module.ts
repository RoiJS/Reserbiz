import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../../../shared/shared.module';

import { TenantContactPersonEditComponent } from './tenant-contact-person-edit.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantContactPersonEditComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TenantContactPersonEditComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantContactPersonEditModule {}
