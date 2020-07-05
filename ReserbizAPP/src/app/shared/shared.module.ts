import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { ActionBarComponent } from './ui/action-bar/action-bar.component';
import { LoadingIndicatorComponent } from './ui/loading-indicator/loading-indicator.component';

@NgModule({
  imports: [RouterModule, CommonModule],
  declarations: [ActionBarComponent, LoadingIndicatorComponent],
  exports: [ActionBarComponent, LoadingIndicatorComponent]
})
export class SharedModule {}
