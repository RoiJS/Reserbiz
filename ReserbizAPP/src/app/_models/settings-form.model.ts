import { IBaseFormSource } from '../_interfaces/ibase-form-source.interface';

import { BaseForm } from './base-form.model';

export class SettingsFormSource
  extends BaseForm<SettingsFormSource>
  implements IBaseFormSource<SettingsFormSource> {
  constructor(public generateAccountStatementDaysBeforeValue: number) {
    super();
  }

  clone() {
    return new SettingsFormSource(this.generateAccountStatementDaysBeforeValue);
  }
}
