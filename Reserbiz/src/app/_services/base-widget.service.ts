import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class BaseWidgetService {
  protected _listItemCount = new BehaviorSubject<number>(0);
  protected _isBusy = new BehaviorSubject<boolean>(false);

  constructor() {}

  get listItemCount(): BehaviorSubject<number> {
    return this._listItemCount;
  }
  get isBusy(): BehaviorSubject<boolean> {
    return this._isBusy;
  }
}
