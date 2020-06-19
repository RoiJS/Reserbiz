import { NgModule } from '@angular/core';

import { NativeScriptRouterModule } from 'nativescript-angular/router';

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
})
export class TermsAddMiscellaneousAddModule {}
