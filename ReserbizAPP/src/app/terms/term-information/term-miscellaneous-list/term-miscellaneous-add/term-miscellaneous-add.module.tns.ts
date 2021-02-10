import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { TermMiscellaneousAddComponent } from './term-miscellaneous-add.component';
import { SharedModule } from '@src/app/shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermMiscellaneousAddComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TermMiscellaneousAddComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TermMiscellaneousAddModule {}
