import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../../shared/shared.module';

import { TenantContractsListComponent } from './tenant-contracts-list.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TenantContractsListComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TenantContractsListComponent],
})
export class TenantContractListModule {}
