import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { TenantContactPersonEditComponent } from './tenant-contact-person-edit.component';

import { SharedModule } from '@src/app/shared/shared.module';

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
