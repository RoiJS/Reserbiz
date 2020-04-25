import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { TenantDetailsTabComponent } from './tenant-details-tab.component';

const routes: Routes = [
  {
    path: 'details-tabs',
    children: [
      {
        path: '',
        component: TenantDetailsTabComponent,
      },
      {
        path: 'edit',
        loadChildren: () =>
          import('./tenant-edit-details/tenant-edit-details.module').then(
            (m) => m.TenantEditDetailsModule
          ),
      },
    ],
  }
];

@NgModule({
  imports: [NativeScriptRouterModule.forChild(routes)],
  exports: [NativeScriptRouterModule],
})
export class TenantDetailsRoutingModule {}
