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
    path: 'forgot-password',
    loadChildren: () =>
      import('./forgot-password/forgot-password.module').then(
        (m) => m.ForgotPasswordModule
      ),
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
      import('./profile/profile.module').then((m) => m.ProfileModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'settings',
    loadChildren: () =>
      import('./settings/settings.module').then((m) => m.SettingsModule),
    canLoad: [AuthGuard],
  },
  {
    path: 'no-connection',
    loadChildren: () =>
      import('./no-connection/no-connection.module').then(
        (m) => m.NoConnectionModule
      ),
  },
  {
    path: 'system-update',
    loadChildren: () =>
      import('./system-update/system-update.module').then(
        (m) => m.SystemUpdateModule
      ),
  },
  {
    path: 'notifications',
    loadChildren: () =>
      import('./notifications/notifications.module').then(
        (m) => m.NotificationsModule
      ),
  },
];
