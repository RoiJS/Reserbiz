import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { SettingsComponent } from './settings.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: SettingsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [SettingsComponent],
})
export class SettingsModule {}
