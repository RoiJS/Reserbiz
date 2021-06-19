import { DurationEnum } from '../_enum/duration-unit.enum';
import { MiscellaneousDueDateEnum } from '../_enum/miscellaneous-due-date.enum';
import { ValueTypeEnum } from '../_enum/value-type.enum';

import { Entity } from './entity.model';
import { TermMiscellaneous } from './term-miscellaneous.model';
import { SpaceType } from './space-type.model';

import { NumberFormatter } from '../_helpers/formatters/number-formatter.helper';

export class Term extends Entity {
  public code: string;
  public termParentId: number;
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
  public depositPaymentDurationText: string;

  public excludeElectricBill: boolean;
  public electricBillAmount: number;
  public excludeWaterBill: boolean;
  public waterBillAmount: number;

  public termMiscellaneous: TermMiscellaneous[];
  public deletedTermMiscellaneous: TermMiscellaneous[];

  public penaltyValue: number;
  public penaltyAmount: number;
  public penaltyAmountText: string;
  public penaltyValueType: ValueTypeEnum;
  public penaltyAmountPerDurationUnit: DurationEnum;
  public penaltyAmountPerDurationUnitText: string;

  public penaltyEffectiveAfterDurationValue: number;
  public penaltyEffectiveText: string;
  public penaltyEffectiveAfterDurationUnit: DurationEnum;
  public penaltyEffectiveAfterDurationUnitText: string;
  public generateAccountStatementDaysBeforeValue: number;
  public miscellaneousDueDate: MiscellaneousDueDateEnum;

  constructor() {
    super();
    this.durationUnit = DurationEnum.Month;
    this.penaltyValueType = ValueTypeEnum.Fixed;
    this.penaltyAmountPerDurationUnit = DurationEnum.Day;
    this.penaltyEffectiveAfterDurationUnit = DurationEnum.Day;
    this.termMiscellaneous = [];
  }

  get formattedRate(): string {
    return NumberFormatter.formatCurrency(this.rate);
  }

  get formattedElectricBillAmount(): string {
    return NumberFormatter.formatCurrency(this.electricBillAmount);
  }

  get formattedWaterBillAmount(): string {
    return NumberFormatter.formatCurrency(this.waterBillAmount);
  }

  get formattedPenaltyAmount(): string {
    return NumberFormatter.formatCurrency(this.penaltyAmount);
  }

  get isChildCopy(): boolean {
    return this.termParentId > 0;
  }

  hasContent() {
    const hasContent = Boolean(
      this.code || this.name || this.spaceTypeId || this.rate
    );

    return hasContent;
  }
}
