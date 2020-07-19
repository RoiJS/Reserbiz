import { NgModule } from '@angular/core';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

import { SharedModule } from '../../../shared/shared.module';
import { TermEditDetailsComponent } from './term-edit-details.component';

@NgModule({
  imports: [
    NativeScriptRouterModule.forChild([
      {
        path: '',
        component: TermEditDetailsComponent,
      },
    ]),
    SharedModule,
  ],
  declarations: [TermEditDetailsComponent],
})
export class TermEditDetailsModule {}
