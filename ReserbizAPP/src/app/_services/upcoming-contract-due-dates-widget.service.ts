import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { MonthOptions } from '../_enum/month-options.enum';

@Injectable({
  providedIn: 'root',
})
export class UpcomingContractDueDatesWidgetService {
  private _selectedMonth = new BehaviorSubject<MonthOptions>(
    new Date().getMonth() + 1
  );
  private _listItemCount = new BehaviorSubject<number>(0);
  private _isBusy = new BehaviorSubject<boolean>(false);
  constructor() {}

  get selectedMonth(): BehaviorSubject<MonthOptions> {
    return this._selectedMonth;
  }
  get listMonthCount(): BehaviorSubject<number> {
    return this._listItemCount;
  }
  get isBusy(): BehaviorSubject<boolean> {
    return this._isBusy;
  }
}
