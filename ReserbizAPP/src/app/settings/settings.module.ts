import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { SettingsRoutingModule } from './settings-routing.module.tns';
import { SettingsComponent } from './settings.component';

@NgModule({
  imports: [SharedModule, SettingsRoutingModule],
  declarations: [SettingsComponent]
})
export class SettingsModule {}
