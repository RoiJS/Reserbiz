import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { TenantAddContactPersonAddComponent } from './tenant-add-contact-person-add.component';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../../../shared/shared.module';

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
