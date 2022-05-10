import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { SharedModule } from '../../shared/shared.module';

import { TermInformationComponent } from './term-information.component';
import { TermDetailsPanelComponent } from './term-details-panel/term-details-panel.component';
import { TermMiscellaneousListPanelComponent } from './term-miscellaneous-list-panel/term-miscellaneous-list-panel.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermInformationComponent,
      },
      {
        path: 'edit',
        loadChildren: () =>
          import('./term-edit-details/term-edit-details.module').then(
            (m) => m.TermEditDetailsModule
          ),
      },
      {
        path: 'term-miscellaneous-list',
        loadChildren: () =>
          import(
            './term-miscellaneous-list/term-miscellaneous-list.module'
          ).then((m) => m.TermMiscellaneousListModule),
      },
    ]),
    SharedModule,
  ],
  declarations: [
    TermInformationComponent,
    TermDetailsPanelComponent,
    TermMiscellaneousListPanelComponent,
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class TermInformationModule {}
