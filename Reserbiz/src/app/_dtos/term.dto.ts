import { IBaseDto } from "~/app/_interfaces/ibase-dto.interface";
import { DurationEnum } from "~/app/_enum/duration-unit.enum";
import { ValueTypeEnum } from "~/app/_enum/value-type.enum";
import { MiscellaneousDueDateEnum } from "~/app/_enum/miscellaneous-due-date.enum";
import { YesNoEnum } from "~/app/_enum/yesno-unit.enum";

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
    public excludeElectricBill: YesNoEnum,
    public electricBillAmount: number,
    public excludeWaterBill: YesNoEnum,
    public waterBillAmount: number,
    public penaltyValue: number,
    public penaltyValueType: ValueTypeEnum,
    public penaltyAmountPerDurationUnit: DurationEnum,
    public penaltyEffectiveAfterDurationValue: number,
    public penaltyEffectiveAfterDurationUnit: DurationEnum,
    public generateAccountStatementDaysBeforeValue: DurationEnum,
    public autoSendNewAccountStatement: YesNoEnum,
    public miscellaneousDueDate: MiscellaneousDueDateEnum,
    public includeMiscellaneousCheckAndCalculateForPenalty: YesNoEnum
  ) {}
}
