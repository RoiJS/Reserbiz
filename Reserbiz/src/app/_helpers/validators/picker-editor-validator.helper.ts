import { PropertyValidator } from 'nativescript-ui-dataform';

export class PickerEditorValidator extends PropertyValidator {
  constructor() {
    super();
  }

  validate(value: any, propertyName: string) {
    return value !== 0;
  }
}
