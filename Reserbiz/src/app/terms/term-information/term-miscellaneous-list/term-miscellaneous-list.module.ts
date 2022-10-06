import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptRouterModule } from '@nativescript/angular';

import { TermMiscellaneousListComponent } from './term-miscellaneous-list.component';

import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermMiscellaneousListComponent,
      },
      {
        path: ':termId/add',
        loadChildren: () =>
          import('./term-miscellaneous-add/term-miscellaneous-add.module').then(
            (m) => m.TermMiscellaneousAddModule
          ),
      },
      {
        path: ':termMiscellaneousId',
        loadChildren: () =>
          import(
            './term-miscellaneous-edit/term-miscellaneous-edit.module'
          ).then((m) => m.TermMiscellaneousEditModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [TermMiscellaneousListComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class TermMiscellaneousListModule {}
