import { Routes } from '@angular/router';
import { SettingsComponent } from './settings/settings.component';

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
