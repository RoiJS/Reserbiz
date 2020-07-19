import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../shared/shared.module';
import { TermAddComponent } from './term-add.component';
import { TermsDetailsFormComponent } from './terms-details-form/terms-details-form.component';
import { TermsAddMiscellaneousComponent } from './terms-add-miscellaneous-list/terms-add-miscellaneous-list.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermAddComponent,
      },
      {
        path: 'add-miscellaneous',
        loadChildren: () =>
          import(
            './terms-add-miscellaneous-list/terms-add-miscellaneous-add/terms-add-miscellaneous-add.module'
          ).then((m) => m.TermsAddMiscellaneousAddModule),
      },
      {
        path: 'edit-miscellaneous/:termMiscellaneousId',
        loadChildren: () =>
          import(
            './terms-add-miscellaneous-list/terms-add-miscellaneous-edit/terms-add-miscellaneous-edit.module'
          ).then((m) => m.TermsAddMiscellaneousEditModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [
    TermAddComponent,
    TermsDetailsFormComponent,
    TermsAddMiscellaneousComponent,
  ],
})
export class TermAddModule {}
