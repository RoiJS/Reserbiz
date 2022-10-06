import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { TermsRoutingModule } from './terms-routing.module';
import { TermsListComponent } from './terms-list/terms-list.component';

@NgModule({
  imports: [SharedModule, TermsRoutingModule],
  declarations: [TermsListComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class TermsModule {}
