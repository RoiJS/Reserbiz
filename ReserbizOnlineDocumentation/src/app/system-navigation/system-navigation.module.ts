import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';

import { SystemNavigationComponent } from './system-navigation.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: SystemNavigationComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [SystemNavigationComponent],
})
export class SystemNavigationModule {}
