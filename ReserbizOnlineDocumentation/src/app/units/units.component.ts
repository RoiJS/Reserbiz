import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { UIService } from '../services/ui.service';
import { SharedComponent } from '../shared/components/shared/shared.component';

@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.scss'],
})
export class UnitsComponent
  extends SharedComponent
  implements OnInit, OnDestroy, AfterViewInit, AfterViewChecked
{
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected uiService: UIService,
    private translateService: TranslateService
  ) {
    super(uiService);
    this.activatedRoute = activatedRoute;
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.setDataSource();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

  ngAfterViewChecked(): void {
    super.ngAfterViewChecked();
  }

  setDataSource(): void {
    this.dataSource = [
      {
        name: this.translateService.instant(
          'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DATA_TYPE'
        ),

        definition: `
          ${this.translateService.instant(
            'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DESCRIPTION'
          )}
          ${this.translateService.instant(
            'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DESCRIPTION_2'
          )}
          ${this.translateService.instant(
            'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.STATUS_INFORMATION.DESCRIPTION_3'
          )}
        `,
      },
      {
        name: this.translateService.instant(
          'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.UNIT_TYPE_INFORMATION.NAME'
        ),
        defaultValue: this.translateService.instant(
          'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.UNIT_TYPE_INFORMATION.DEFAULT_VALUES'
        ),
        datatype: this.translateService.instant(
          'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.UNIT_TYPE_INFORMATION.DATA_TYPE'
        ),
        definition: this.translateService.instant(
          'UNITS_PAGE.BODY.FILTER_UNIT_SECTION.TABLE_INFORMATION.UNIT_TYPE_INFORMATION.DESCRIPTION'
        ),
      },
    ];
  }
}
