import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { TranslateService } from '@ngx-translate/core';

import { DataFormEventData } from 'nativescript-ui-dataform';

import { UpcomingContractDueDatesMonthPickerFormSource } from '@src/app/_models/form/upcoming-contract-due-dates-month-picker-form.model';

import { UpcomingContractDueDatesWidgetService } from '@src/app/_services/upcoming-contract-due-dates-widget.service';

import { MonthOptions } from '@src/app/_enum/month-options.enum';

import { MonthValueProvider } from '@src/app/_helpers/value_providers/month-value-provider.helper';
import { MonthOption } from '@src/app/_models/options/month-option.model';

@Component({
  selector: 'ns-upcoming-contract-due-dates-month-picker',
  templateUrl: './upcoming-contract-due-dates-month-picker.component.html',
  styleUrls: ['./upcoming-contract-due-dates-month-picker.component.scss'],
})
export class UpcomingContractDueDatesMonthPickerComponent
  implements OnInit, OnDestroy {
  private _formSource: UpcomingContractDueDatesMonthPickerFormSource;

  private _monthValueProvider: MonthValueProvider;
  private _selectedMonthSub: Subscription;

  constructor(
    private translateService: TranslateService,
    private upcomingContractDueDateService: UpcomingContractDueDatesWidgetService
  ) {}

  ngOnInit() {
    this._monthValueProvider = new MonthValueProvider(this.translateService);

    this._selectedMonthSub = this.upcomingContractDueDateService.selectedMonth.subscribe(
      (selectedMonth: MonthOptions) => {
        this._formSource = new UpcomingContractDueDatesMonthPickerFormSource(
          selectedMonth
        );
      }
    );
  }

  ngOnDestroy() {
    if (this._selectedMonthSub) {
      this._selectedMonthSub.unsubscribe();
    }
  }

  onPropertyCommitted(args: DataFormEventData) {
    if (args.propertyName === 'month') {
      this.upcomingContractDueDateService.selectedMonth.next(this._formSource.month);
    }
  }

  get formSource(): UpcomingContractDueDatesMonthPickerFormSource {
    return this._formSource;
  }

  get monthOptions(): Array<MonthOption> {
    return this._monthValueProvider.monthOptions;
  }
}
