import { Routes } from '@angular/router';
import { TermsListComponent } from './terms-list/terms-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'terms-list',
    pathMatch: 'full'
  },
  {
    path: 'terms-list',
    component: TermsListComponent
  }
];
