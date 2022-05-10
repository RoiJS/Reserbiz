import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../../shared/shared.module';

import { TermEditDetailsComponent } from './term-edit-details.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermEditDetailsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TermEditDetailsComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TermEditDetailsModule {}
