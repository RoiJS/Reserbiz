import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '@src/app/shared/shared.module';

import { TenantAddContactPersonAddComponent } from './tenant-add-contact-person-add.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantAddContactPersonAddComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TenantAddContactPersonAddComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantAddContactPersonAddModule {}
