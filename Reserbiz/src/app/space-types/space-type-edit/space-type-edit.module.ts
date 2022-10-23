import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';
import { SpaceTypeEditComponent } from './space-type-edit.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: SpaceTypeEditComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [SpaceTypeEditComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SpaceTypeEditModule {}
