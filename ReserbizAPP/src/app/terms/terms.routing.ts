import { Routes } from '@angular/router';
import { TermsListComponent } from './terms-list/terms-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'terms-list',
    pathMatch: 'full',
  },
  {
    path: 'terms-list',
    component: TermsListComponent,
  },
  {
    path: 'term-add',
    loadChildren: () =>
      import('./term-add/term-add.module').then((m) => m.TermAddModule),
  },
  {
    path: ':termId',
    loadChildren: () =>
      import('./term-information/term-information.module').then(
        (m) => m.TermInformationModule
      ),
  },
];
