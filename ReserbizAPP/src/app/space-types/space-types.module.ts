import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { SpaceTypesRoutingModule } from './space-types-routing.module.tns';
import { SpaceTypesListComponent } from './space-types-list/space-types-list.component';

@NgModule({
  imports: [SharedModule, SpaceTypesRoutingModule],
  declarations: [SpaceTypesListComponent]
})
export class SpaceTypesModule {}
