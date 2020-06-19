import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { TermInformationComponent } from './term-information.component';
import { SharedModule } from '../../shared/shared.module';
import { TermDetailsPanelComponent } from './term-details-panel/term-details-panel.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermInformationComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TermInformationComponent, TermDetailsPanelComponent],
})
export class TermInformationModule {}
