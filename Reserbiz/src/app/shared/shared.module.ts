import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import {
  NativeScriptCommonModule,
  NativeScriptRouterModule,
} from '@nativescript/angular';
import { TranslateModule } from '@ngx-translate/core';

import { ActionBarComponent } from '../shared/ui/action-bar/action-bar.component';
import { LoaderLayoutComponent } from '../shared/ui/loader-layout/loader-layout.component';
import { ListLayoutComponent } from '../shared/ui/list-layout/list-layout.component';
import { FloatingButtonComponent } from '../shared/ui/floating-button/floating-button.component';

import { NativeScriptUIDataFormModule } from 'nativescript-ui-dataform/angular';
import { NativeScriptUIListViewModule } from 'nativescript-ui-listview/angular';
import { NativeScriptUIChartModule } from 'nativescript-ui-chart/angular';
import { NativeScriptDateTimePickerModule } from '@nativescript/datetimepicker/angular';

@NgModule({
  imports: [
    NativeScriptCommonModule,
    NativeScriptRouterModule,
    NativeScriptUIDataFormModule,
    NativeScriptUIListViewModule,
    NativeScriptUIChartModule,
    NativeScriptDateTimePickerModule,
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
    NativeScriptDateTimePickerModule,
    TranslateModule,
  ],
  schemas: [NO_ERRORS_SCHEMA],
})
export class SharedModule {}
