import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './spaces.routing';

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SpacesRoutingModule {}
