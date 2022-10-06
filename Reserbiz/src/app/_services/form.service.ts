import { Injectable } from "@angular/core";
import { TextField } from "@nativescript/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class FormService {
  constructor() {}

  private _isFormValid = new BehaviorSubject<boolean>(false);

  dismiss(inputFields: TextField[]) {
    inputFields.forEach((inf) => inf.focus());
    inputFields[inputFields.length - 1].dismissSoftInput();
  }

  get isFormValid() {
    return this._isFormValid;
  }
}
