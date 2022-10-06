import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';
import { SpaceTypeAddComponent } from './space-type-add.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: SpaceTypeAddComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [SpaceTypeAddComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class SpaceTypeAddModule {}
