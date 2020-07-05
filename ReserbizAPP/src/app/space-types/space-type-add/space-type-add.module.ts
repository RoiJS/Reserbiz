import { NgModule } from '@angular/core';

import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SpaceTypeAddComponent } from './space-type-add.component';
import { SharedModule } from '../../shared/shared.module';

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
})
export class SpaceTypeAddModule {}
