import { Routes } from '@angular/router';
import { SpaceListComponent } from './space-list/space-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'space-list',
    pathMatch: 'full',
  },
  {
    path: 'space-add',
    loadChildren: () =>
      import('./space-add/space-add.module').then((m) => m.SpaceAddModule),
  },
  {
    path: 'space-list',
    component: SpaceListComponent,
  },
  {
    path: 'space-edit/:id',
    loadChildren: () =>
      import('./space-edit/space-edit.module').then(
        (m) => m.SpaceEditModule
      ),
  },
];
