import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NoConnectionComponent } from './no-connection.component';

const routes: Routes = [
  {
    path: '',
    component: NoConnectionComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NoConnectionRoutingModule {}
