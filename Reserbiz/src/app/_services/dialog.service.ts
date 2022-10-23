import { Injectable } from '@angular/core';

import { Dialogs } from '@nativescript/core';

import { ButtonOptions } from '~/app/_enum/button-options.enum';

@Injectable({ providedIn: 'root' })
export class DialogService {

  constructor() {}

  alert(title: string, message: string) {
    const alertOptions = {
      title: title,
      message: message,
      okButtonText: ButtonOptions.OKAY,
    }

    return Dialogs.alert(alertOptions);
  }

  confirm(title: string, message: string) {
    const confirmOptions = {
      title: title,
      message: message,
      okButtonText: ButtonOptions.YES,
      cancelButtonText: ButtonOptions.NO,
    }

    return Dialogs.confirm(confirmOptions);
  }
}
