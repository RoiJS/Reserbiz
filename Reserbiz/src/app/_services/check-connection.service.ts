import { Injectable } from '@angular/core';
import { connectionType } from '@nativescript/core/connectivity';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CheckConnectionService {
  private _currentConnectionType = new BehaviorSubject<connectionType>(
    connectionType.none
  );
  constructor() {}

  get currentConnectionType(): BehaviorSubject<connectionType> {
    return this._currentConnectionType;
  }
}
