import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

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
  schemas: [NO_ERRORS_SCHEMA]
})
export class PenaltiesModule {}
