import { Injectable, ViewContainerRef } from '@angular/core';
import { ios } from '@nativescript/core/application';
import { BehaviorSubject } from 'rxjs';
import { ad } from '@nativescript/core/utils/utils';

@Injectable({ providedIn: 'root' })
export class UIService {
  private _drawerState = new BehaviorSubject<void>(null);
  private _rootVCRef: ViewContainerRef;

  get drawerState() {
    return this._drawerState.asObservable();
  }

  toggleDrawer() {
    this._drawerState.next(null);
  }

  setRootVCRef(vcRef: ViewContainerRef) {
    this._rootVCRef = vcRef;
  }

  getRootVCRef() {
    return this._rootVCRef;
  }

  hideKeyboard(): void {
    if (ios) {
      ios.nativeApp.sendActionToFromForEvent(
        'resignFirstResponder',
        null,
        null,
        null
      );
    } else {
      ad.dismissSoftInput();
    }
  }
}
