import { RadDataForm } from 'nativescript-ui-dataform';
import { IBaseFormSource } from '../_interfaces/ibase-form-source.interface';
import { BaseForm } from '../_models/base-form.model';

export abstract class BaseFormHelper<
  TFormSource extends BaseForm<TFormSource>
> {
  constructor() {}

  reloadFormSource(formSource: TFormSource, newValues: any): TFormSource  {
    if (newValues) {
      const clonedFormSource = formSource.clone();
      for (const propName in newValues) {
        if (newValues.hasOwnProperty(propName)) {
          clonedFormSource[propName] = newValues[propName];
        }
      }
      return clonedFormSource;
    }
  }
}
