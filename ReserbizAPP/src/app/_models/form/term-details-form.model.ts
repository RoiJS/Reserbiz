import { BaseForm } from './base-form.model';
import { DurationEnum } from '../../_enum/duration-unit.enum';
import { ValueTypeEnum } from '../../_enum/value-type.enum';

export class TermDetailsFormSource extends BaseForm<TermDetailsFormSource> {
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
  ) {
    super();
  }

  clone() {
    return new TermDetailsFormSource(
      this.code,
      this.name,
      this.spaceTypeId,
      this.rate,
      this.maximumNumberOfOccupants,
      this.durationUnit,
      this.advancedPaymentDurationValue,
      this.depositPaymentDurationValue,
      this.excludeElectricBill,
      this.electricBillAmount,
      this.excludeWaterBill,
      this.waterBillAmount,
      this.penaltyValue,
      this.penaltyValueType,
      this.penaltyAmountPerDurationUnit,
      this.penaltyEffectiveAfterDurationValue,
      this.penaltyEffectiveAfterDurationUnit
    );
  }
}
