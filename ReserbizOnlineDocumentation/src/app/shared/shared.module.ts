import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';

import { TranslateModule } from '@ngx-translate/core';

import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTreeModule } from '@angular/material/tree';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTableModule } from '@angular/material/table';

import { ToolBarComponent } from './tool-bar/tool-bar.component';
import { FooterComponent } from '../footer/footer.component';
import { SafeHtmlPipe } from '../pipes/safe-html.pipe';

@NgModule({
  imports: [
    CommonModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatTreeModule,
    MatCardModule,
    MatGridListModule,
    MatExpansionModule,
    MatTableModule,
    TranslateModule.forChild(),
  ],
  declarations: [ToolBarComponent, FooterComponent, SafeHtmlPipe],
  exports: [
    CommonModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatTreeModule,
    MatCardModule,
    MatGridListModule,
    MatExpansionModule,
    MatTableModule,
    SafeHtmlPipe,

    TranslateModule,
    ToolBarComponent,
    FooterComponent,
  ],
})
export class SharedModule {}
