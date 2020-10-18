import { Routes } from '@angular/router';

import { AuthGuard } from './_guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'tenants',
    loadChildren: () =>
      import('./tenants/tenants.module').then((m) => m.TenantsModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'contracts',
    loadChildren: () =>
      import('./contracts/contracts.module').then((m) => m.ContractsModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'terms',
    loadChildren: () =>
      import('./terms/terms.module').then((m) => m.TermsModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'space-types',
    loadChildren: () =>
      import('./space-types/space-types.module').then(
        (m) => m.SpaceTypesModule
      ),
    canLoad: [AuthGuard],
  },
  {
    path: 'spaces',
    loadChildren: () =>
      import('./spaces/spaces.module').then((m) => m.SpacesModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'profile',
    loadChildren: () =>
      import('./profile/profile.module.tns').then((m) => m.ProfileModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'settings',
    loadChildren: () =>
      import('./settings/settings.module').then((m) => m.SettingsModule),
    canLoad: [AuthGuard],
  },
];
