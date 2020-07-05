import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TenantListComponent } from './tenant-list/tenant-list.component';

const routes: Routes = [
  {
    path: '',
    component: TenantListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TenantRoutingModule {}
