import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { SpaceTypesRoutingModule } from './space-types-routing.module';
import { SpaceTypesListComponent } from './space-types-list/space-types-list.component';

@NgModule({
  imports: [SharedModule, SpaceTypesRoutingModule],
  declarations: [SpaceTypesListComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SpaceTypesModule {}
