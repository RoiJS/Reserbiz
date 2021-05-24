import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/overview',
    pathMatch: 'full',
  },
  {
    path: 'overview',
    loadChildren: () =>
      import('./overview/overview.module').then((m) => m.OverviewModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
