import { Injectable } from '@angular/core';
import { alert } from 'tns-core-modules/ui/dialogs';

import {
  CFAlertDialog,
  DialogOptions,
  CFAlertStyle,
  CFAlertActionStyle,
  CFAlertActionAlignment
} from 'nativescript-cfalert-dialog';
import { ButtonOptions } from '../_enum/button-options.enum';

@Injectable({ providedIn: 'root' })
export class DialogService {
  private _cfalertDialog = new CFAlertDialog();

  constructor() {}

  alert(title: string, message: string, onClickEvent: Function = null) {
    const alertOptions: DialogOptions = {
      dialogStyle: CFAlertStyle.BOTTOM_SHEET,
      title: title,
      message: message,
      backgroundBlur: true,
      cancellable: true,
      buttons: [
        {
          text: ButtonOptions.OKAY,
          buttonStyle: CFAlertActionStyle.POSITIVE,
          buttonAlignment: CFAlertActionAlignment.END,
          textColor: '#FFFFFF',
          backgroundColor: '#3A53FF',
          onClick: function() {
            if (onClickEvent) {
              onClickEvent();
            }
          }
        }
      ]
    };

    this._cfalertDialog.show(alertOptions);
  }

  confirm(title: string, message: string) {
    const confirmOptions: DialogOptions = {
      dialogStyle: CFAlertStyle.BOTTOM_SHEET,
      title: title,
      message: message,
      buttons: [
        {
          text: ButtonOptions.YES,
          buttonStyle: CFAlertActionStyle.POSITIVE,
          backgroundColor: '#3A53FF',
          buttonAlignment: CFAlertActionAlignment.JUSTIFIED,
          onClick: function() {}
        },
        {
          text: ButtonOptions.NO,
          buttonStyle: CFAlertActionStyle.NEGATIVE,
          backgroundColor: '#D7D7D7',
          buttonAlignment: CFAlertActionAlignment.JUSTIFIED,
          onClick: function() {}
        }
      ]
    };

    return this._cfalertDialog.show(confirmOptions);
  }
}
