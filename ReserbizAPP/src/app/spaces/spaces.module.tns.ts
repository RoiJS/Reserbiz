import { NgModule } from '@angular/core';
import { SpaceListComponent } from './space-list/space-list.component';

import { SharedModule } from '../shared/shared.module';
import { SpacesRoutingModule } from './spaces-routing.module';

@NgModule({
  imports: [SharedModule, SpacesRoutingModule],
  declarations: [SpaceListComponent],
})
export class SpacesModule {}
