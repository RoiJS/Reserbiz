import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { TermsAddMiscellaneousEditComponent } from './terms-add-miscellaneous-edit.component';
import { SharedModule } from '../../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      { path: '', component: TermsAddMiscellaneousEditComponent },
    ]),
    SharedModule,
  ],
  declarations: [TermsAddMiscellaneousEditComponent],
})
export class TermsAddMiscellaneousEditModule {}
