import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { TenantContactPersonAddComponent } from './tenant-contact-person-add.component';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantContactPersonAddComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TenantContactPersonAddComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantContactPersonAddModule {}
