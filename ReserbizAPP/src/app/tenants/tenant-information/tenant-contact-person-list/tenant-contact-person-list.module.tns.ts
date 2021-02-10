import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '@src/app/shared/shared.module';

import { TenantContactPersonListComponent } from './tenant-contact-person-list.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantContactPersonListComponent,
      },
      {
        path: ':tenantId/add',
        loadChildren: () =>
          import(
            './tenant-contact-person-add/tenant-contact-person-add.module'
          ).then((m) => m.TenantContactPersonAddModule),
      },
      {
        path: ':contactPersonId',
        loadChildren: () =>
          import(
            './tenant-contact-person-edit/tenant-contact-person-edit.module'
          ).then((m) => m.TenantContactPersonEditModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [TenantContactPersonListComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantContactPersonListModule {}
