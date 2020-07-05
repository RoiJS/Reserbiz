import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { TenantInformationComponent } from './tenant-information.component';
import { TenantDetailsPanelComponent } from './tenant-details-panel/tenant-details-panel.component';
import { TenantContactPersonListPanelComponent } from './tenant-contact-person-list-panel/tenant-contact-person-list-panel.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantInformationComponent,
      },
      {
        path: 'edit',
        loadChildren: () =>
          import('./tenant-edit-details/tenant-edit-details.module').then(
            (m) => m.TenantEditDetailsModule
          ),
      },
      {
        path: 'contact-person-list',
        loadChildren: () =>
          import(
            './tenant-contact-person-list/tenant-contact-person-list.module'
          ).then((m) => m.TenantContactPersonListModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [
    TenantInformationComponent,
    TenantDetailsPanelComponent,
    TenantContactPersonListPanelComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TenantInformationModule {}
