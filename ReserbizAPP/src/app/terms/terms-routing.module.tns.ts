import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './terms.routing';

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TermsRoutingModule {}
