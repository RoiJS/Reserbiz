import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { TenantAddContactPersonEditComponent } from './tenant-add-contact-person-edit.component';
import { SharedModule } from '@src/app/shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantAddContactPersonEditComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TenantAddContactPersonEditComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantAddContactPersonEditModule {}
