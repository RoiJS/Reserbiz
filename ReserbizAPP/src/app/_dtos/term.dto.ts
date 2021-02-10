import { IBaseDto } from '../_interfaces/ibase-dto.interface';
import { DurationEnum } from '../_enum/duration-unit.enum';
import { ValueTypeEnum } from '../_enum/value-type.enum';

export class TermDto implements IBaseDto {
  constructor(
    public code: string,
    public name: string,
    public spaceTypeId: number,
    public rate: number,
    public maximumNumberOfOccupants: number,
    public durationUnit: DurationEnum,
    public advancedPaymentDurationValue: number,
    public depositPaymentDurationValue: number,
    public excludeElectricBill: boolean,
    public electricBillAmount: number,
    public excludeWaterBill: boolean,
    public waterBillAmount: number,
    public penaltyValue: number,
    public penaltyValueType: ValueTypeEnum,
    public penaltyAmountPerDurationUnit: DurationEnum,
    public penaltyEffectiveAfterDurationValue: number,
    public penaltyEffectiveAfterDurationUnit: DurationEnum
  ) {}
}
