import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular';

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
})
export class SpaceAddModule {}
