import { BaseForm } from './base-form.model';
import { UnitStatusEnum } from '@src/app/_enum/unit-status.enum';

export class UnitFilterFormSource extends BaseForm<UnitFilterFormSource> {
  constructor(public status: UnitStatusEnum, public unitTypeId: number) {
    super();
  }

  clone() {
    return new UnitFilterFormSource(this.status, this.unitTypeId);
  }
}
