import { UnitStatusEnum } from '@src/app/_enum/unit-status.enum';

export interface IUnitStatusValueProvider {
  statusOptions: Array<{ key: UnitStatusEnum; label: string }>;
}
