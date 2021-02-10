import { NgModule } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '@src/app/shared/shared.module';
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
})
export class SpaceTypeEditModule {}
