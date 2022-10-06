import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';
import { SpaceAddComponent } from './space-add.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      { path: '', component: SpaceAddComponent },
    ]),
    SharedModule,
  ],
  declarations: [SpaceAddComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SpaceAddModule {}
