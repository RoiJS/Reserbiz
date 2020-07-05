import { Routes } from '@angular/router';
import { SpaceTypesListComponent } from './space-types-list/space-types-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'space-type-list',
    pathMatch: 'full',
  },
  {
    path: 'space-type-list',
    component: SpaceTypesListComponent,
  },
  {
    path: 'space-type-add',
    loadChildren: () =>
      import('./space-type-add/space-type-add.module').then(
        (m) => m.SpaceTypeAddModule
      ),
  },
  {
    path: 'space-type-edit/:id',
    loadChildren: () =>
      import('./space-type-edit/space-type-edit.module').then(
        (m) => m.SpaceTypeEditModule
      ),
  },
];
