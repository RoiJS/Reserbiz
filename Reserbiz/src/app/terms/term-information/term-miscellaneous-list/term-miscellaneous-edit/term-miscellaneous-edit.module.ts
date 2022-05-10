import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { TermMiscellaneousEditComponent } from './term-miscellaneous-edit.component';
import { SharedModule } from '../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermMiscellaneousEditComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TermMiscellaneousEditComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TermMiscellaneousEditModule {}
