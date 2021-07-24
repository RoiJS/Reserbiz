import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { SpaceListComponent } from './space-list/space-list.component';

import { SharedModule } from '../shared/shared.module';
import { SpacesRoutingModule } from './spaces-routing.module';
import { UnitFilterDialogComponent } from './space-list/unit-filter-dialog/unit-filter-dialog.component';

@NgModule({
  imports: [SharedModule, SpacesRoutingModule],
  entryComponents: [UnitFilterDialogComponent],
  declarations: [SpaceListComponent, UnitFilterDialogComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SpacesModule {}
