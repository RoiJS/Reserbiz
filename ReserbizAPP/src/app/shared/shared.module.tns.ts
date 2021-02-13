import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import {
  NativeScriptCommonModule,
  NativeScriptRouterModule,
} from '@nativescript/angular';
import { TranslateModule } from '@ngx-translate/core';

import { ActionBarComponent } from '@src/app/shared/ui/action-bar/action-bar.component';
import { LoaderLayoutComponent } from '@src/app/shared/ui/loader-layout/loader-layout.component';
import { ListLayoutComponent } from '@src/app/shared/ui/list-layout/list-layout.component';
import { FloatingButtonComponent } from '@src/app/shared/ui/floating-button/floating-button.component';

import { NativeScriptUIDataFormModule } from 'nativescript-ui-dataform/angular';
import { NativeScriptUIListViewModule } from 'nativescript-ui-listview/angular';
import { NativeScriptUIChartModule } from 'nativescript-ui-chart/angular';

@NgModule({
  imports: [
    NativeScriptCommonModule,
    NativeScriptRouterModule,
    NativeScriptUIDataFormModule,
    NativeScriptUIListViewModule,
    NativeScriptUIChartModule,
    TranslateModule.forChild(),
  ],
  declarations: [
    ActionBarComponent,
    LoaderLayoutComponent,
    ListLayoutComponent,
    FloatingButtonComponent,
  ],
  exports: [
    ActionBarComponent,
    LoaderLayoutComponent,
    ListLayoutComponent,
    FloatingButtonComponent,
    NativeScriptCommonModule,
    NativeScriptUIDataFormModule,
    NativeScriptUIListViewModule,
    NativeScriptUIChartModule,
    TranslateModule,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class SharedModule {}
