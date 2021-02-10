import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { TermsAddMiscellaneousEditComponent } from './terms-add-miscellaneous-edit.component';
import { SharedModule } from '@src/app/shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      { path: '', component: TermsAddMiscellaneousEditComponent },
    ]),
    SharedModule,
  ],
  declarations: [TermsAddMiscellaneousEditComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TermsAddMiscellaneousEditModule {}
