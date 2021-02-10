import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { SpaceListComponent } from './space-list/space-list.component';

import { SharedModule } from '../shared/shared.module';
import { SpacesRoutingModule } from './spaces-routing.module';

@NgModule({
  imports: [SharedModule, SpacesRoutingModule],
  declarations: [SpaceListComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SpacesModule {}
