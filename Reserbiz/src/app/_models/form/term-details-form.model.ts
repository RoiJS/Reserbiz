import { BaseForm } from "./base-form.model";
import { DurationEnum } from "~/app/_enum/duration-unit.enum";
import { ValueTypeEnum } from "~/app/_enum/value-type.enum";
import { MiscellaneousDueDateEnum } from "~/app/_enum/miscellaneous-due-date.enum";
import { YesNoEnum } from "~/app/_enum/yesno-unit.enum";

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
    public excludeElectricBill: YesNoEnum,
    public electricBillAmount: number,
    public excludeWaterBill: YesNoEnum,
    public waterBillAmount: number,
    public penaltyValue: number,
    public penaltyValueType: ValueTypeEnum,
    public penaltyAmountPerDurationUnit: DurationEnum,
    public penaltyEffectiveAfterDurationValue: number,
    public penaltyEffectiveAfterDurationUnit: DurationEnum,
    public generateAccountStatementDaysBeforeValue: number,
    public autoSendNewAccountStatement: YesNoEnum,
    public miscellaneousDueDate: MiscellaneousDueDateEnum,
    public includeMiscellaneousCheckAndCalculateForPenalty: YesNoEnum
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
      this.penaltyEffectiveAfterDurationUnit,
      this.generateAccountStatementDaysBeforeValue,
      this.autoSendNewAccountStatement,
      this.miscellaneousDueDate,
      this.includeMiscellaneousCheckAndCalculateForPenalty
    );
  }
}
