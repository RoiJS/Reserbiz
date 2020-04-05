import { NgModule } from '@angular/core';
import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { NativeScriptRouterModule } from 'nativescript-angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { ActionBarComponent } from './ui/action-bar/action-bar.component';
import { LoaderLayoutComponent } from './ui/loader-layout/loader-layout.component';
import { FloatingButtonComponent } from './ui/floating-button/floating-button.component';

@NgModule({
  imports: [
    NativeScriptCommonModule,
    NativeScriptRouterModule,
    TranslateModule.forChild()
  ],
  declarations: [
    ActionBarComponent,
    LoaderLayoutComponent,
    FloatingButtonComponent
  ],
  exports: [
    ActionBarComponent,
    LoaderLayoutComponent,
    FloatingButtonComponent,
    NativeScriptCommonModule,
    TranslateModule
  ]
})
export class SharedModule {}
