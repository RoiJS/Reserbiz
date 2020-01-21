import { Routes } from '@angular/router';
import { SpaceTypesListComponent } from './space-types-list/space-types-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'space-type-list',
    pathMatch: 'full'
  },
  {
    path: 'space-type-list',
    component: SpaceTypesListComponent
  }
];
