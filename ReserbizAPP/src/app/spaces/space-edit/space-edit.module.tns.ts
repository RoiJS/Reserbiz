import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SpaceEditComponent } from './space-edit.component';

import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: SpaceEditComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [SpaceEditComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SpaceEditModule {}
