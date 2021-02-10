import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { MonthOptions } from '../_enum/month-options.enum';
import { BaseWidgetService } from './base-widget.service';

@Injectable({
  providedIn: 'root',
})
export class UpcomingContractDueDatesWidgetService extends BaseWidgetService {
  private _selectedMonth = new BehaviorSubject<MonthOptions>(
    new Date().getMonth() + 1
  );
  constructor() {
    super();
  }

  get selectedMonth(): BehaviorSubject<MonthOptions> {
    return this._selectedMonth;
  }
}
