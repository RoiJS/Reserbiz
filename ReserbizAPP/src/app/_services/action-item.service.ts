import { Injectable } from '@angular/core';
import { isAndroid, Page } from 'tns-core-modules/ui/page/page';

@Injectable({
  providedIn: 'root',
})
export class ActionItemService {
  constructor() {}
  private _actionItemId: string;
  private _page: Page;

  setPage(page: Page): ActionItemService {
    this._page = page;
    return this;
  }

  setActionItem(actionItemId: string) {
    this._actionItemId = actionItemId;
    return this;
  }

  enable(status: boolean) {
    const actionItem = <any>this._page.getViewById(this._actionItemId);
    if (isAndroid) {
      actionItem.actionBar.nativeViewProtected
        .getMenu()
        .findItem(actionItem._getItemId())
        .setEnabled(status);
    }
  }
}
