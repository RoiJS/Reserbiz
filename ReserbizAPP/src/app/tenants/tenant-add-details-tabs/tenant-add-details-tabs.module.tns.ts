import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '@src/app/shared/shared.module';

import { TenantAddDetailsTabsComponent } from './tenant-add-details-tabs.component';
import { TenantDetailsFormComponent } from './tenant-details-form/tenant-details-form.component';
import { TenantAddContactPersonListComponent } from './tenant-add-contact-person-list/tenant-add-contact-person-list.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantAddDetailsTabsComponent,
      },
      {
        path: 'add-contact-person',
        loadChildren: () =>
          import(
            './tenant-add-contact-person-list/tenant-add-contact-person-add/tenant-add-contact-person-add.module'
          ).then((m) => m.TenantAddContactPersonAddModule),
      },
      {
        path: 'edit-contact-person/:contactPersonId',
        loadChildren: () =>
          import(
            './tenant-add-contact-person-list/tenant-add-contact-person-edit/tenant-add-contact-person-edit.module'
          ).then((m) => m.TenantAddContactPersonEditModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [
    TenantAddDetailsTabsComponent,
    TenantDetailsFormComponent,
    TenantAddContactPersonListComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantAddDetailsTabsModule {}
