import { Injectable } from '@angular/core';
import { TextField } from '@nativescript/core';

@Injectable({
  providedIn: 'root'
})
export class FormService {
  constructor() {}

  dismiss(inputFields: TextField[]) {
    inputFields.forEach(inf => inf.focus());
    inputFields[inputFields.length - 1].dismissSoftInput();
  }
}
