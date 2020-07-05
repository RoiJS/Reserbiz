import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { TermsRoutingModule } from './terms-routing.module.tns';
import { TermsListComponent } from './terms-list/terms-list.component';

@NgModule({
  imports: [SharedModule, TermsRoutingModule],
  declarations: [TermsListComponent]
})
export class TermsModule {}
