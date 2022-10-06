import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { TermsAddMiscellaneousAddComponent } from './terms-add-miscellaneous-add.component';
import { SharedModule } from '../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermsAddMiscellaneousAddComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TermsAddMiscellaneousAddComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class TermsAddMiscellaneousAddModule {}
