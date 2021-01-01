import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular';

import { SharedModule } from '../../../../../shared/shared.module';

import { PenaltiesComponent } from './penalties.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: PenaltiesComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [PenaltiesComponent],
})
export class PenaltiesModule {}
