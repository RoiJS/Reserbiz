import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';

const appversion = require('nativescript-appversion');

@Injectable({
  providedIn: 'root',
})
export class AppVersionService {
  private _appVersion = new BehaviorSubject<string>('');

  constructor(private translate: TranslateService) {
    this.initAppVersion();
  }

  private initAppVersion() {
    appversion.getVersionName().then((version: string) => {
      this._appVersion.next(version);
    });
  }

  get copyRightText(): string {
    return `${String.fromCharCode(0xf1f9)} ${this.translate.instant(
      'SIDE_DRAWER_SECTION.FOOTER.COPYRIGHT'
    )} ${this.translate.instant(
      'SIDE_DRAWER_SECTION.FOOTER.ALL_RIGHTS_RESERVED'
    )}`;
  }

  get appVersion(): string {
    return `${this.translate.instant('SIDE_DRAWER_SECTION.FOOTER.VERSION')} ${
      this._appVersion.value
    }`;
  }
}
