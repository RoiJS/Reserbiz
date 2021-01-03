import { Routes } from '@angular/router';
import { SettingsComponent } from './settings.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'settings',
    pathMatch: 'full'
  },
  {
    path: 'settings',
    component: SettingsComponent
  }
];
