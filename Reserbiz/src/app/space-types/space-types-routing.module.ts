import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './space-types.routing';

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    schemas: [NO_ERRORS_SCHEMA]
})
export class SpaceTypesRoutingModule {}
