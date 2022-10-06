import { UnitStatusEnum } from '../../_enum/unit-status.enum';
import { IEntityFilter } from './ientity-filter.interface';

export interface ISpaceFilter extends IEntityFilter {
  status: UnitStatusEnum;
  unitTypeId: number;
}
