import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { TermEditComponent } from './term-edit.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      { path: '', component: TermEditComponent },
    ]),
    SharedModule,
  ],
  declarations: [TermEditComponent],
})
export class TermEditModule {}
