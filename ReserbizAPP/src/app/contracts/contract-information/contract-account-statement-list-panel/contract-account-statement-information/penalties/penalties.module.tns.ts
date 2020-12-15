import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular';

import { PenaltiesComponent } from './penalties.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: PenaltiesComponent,
      },
    ]),
  ],
  declarations: [PenaltiesComponent],
})
export class PenaltiesModule {}
