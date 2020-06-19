import { DurationEnum } from '../_enum/duration-unit.enum';
import { ValueTypeEnum } from '../_enum/value-type.enum';
import { Entity } from './entity.model';
import { TermMiscellaneous } from './term-miscellaneous.model';
import { SpaceType } from './space-type.model';

export class Term extends Entity {
  public code: string;
  public name: string;
  public spaceTypeId: number;
  public spaceType: SpaceType;
  public rate: number;
  public maximumNumberOfOccupants: number;

  public durationUnit: DurationEnum;
  public durationUnitText: string;
  public advancedPaymentDurationValue: number;
  public advancedPaymentDurationText: string;
  public depositPaymentDurationValue: number;
  public depositPaymentDurationValueText: string;

  public excludeElectricBill: boolean;
  public electricBillAmount: number;
  public excludeWaterBill: boolean;
  public waterBillAmount: number;

  public termMiscellaneous: TermMiscellaneous[];

  public penaltyValue: number;
  public penaltyAmount: number;
  public penaltyValueType: ValueTypeEnum;
  public penaltyAmountPerDurationUnit: DurationEnum;
  public penaltyAmountPerDurationUnitText: string;

  public penaltyEffectiveAfterDurationValue: number;
  public penaltyEffectiveAfterDurationUnit: DurationEnum;
  public penaltyEffectiveAfterDurationUnitText: string;

  constructor() {
    super();
    this.durationUnit = DurationEnum.Month;
    this.penaltyValueType = ValueTypeEnum.Fixed;
    this.penaltyAmountPerDurationUnit = DurationEnum.Day;
    this.penaltyEffectiveAfterDurationUnit = DurationEnum.Day;
    this.termMiscellaneous = [];
  }

  hasContent() {
    const hasContent = !!(
      this.code ||
      this.name ||
      this.spaceTypeId ||
      this.rate
    );

    return hasContent;
  }
}
