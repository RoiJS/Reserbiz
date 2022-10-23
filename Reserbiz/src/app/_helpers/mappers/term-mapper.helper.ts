import { IBaseEntityMapper } from "~/app/_interfaces/mappers/ibase-entity-mapper.interface";

import { TermMiscellaneousMapper } from "./term-miscellaneous-mapper.helper";
import { IBaseDtoEntityMapper } from "~/app/_interfaces/mappers/ibase-dto-entity-mapper.interface";

import { Term } from "~/app/_models/term.model";
import { TermMiscellaneous } from "~/app/_models/term-miscellaneous.model";
import { TermDetailsFormSource } from "~/app/_models/form/term-details-form.model";
import { SpaceType } from "~/app/_models/space-type.model";

import { TermDto } from "~/app/_dtos/term.dto";

import { DurationEnum } from "~/app/_enum/duration-unit.enum";
import { ValueTypeEnum } from "~/app/_enum/value-type.enum";
import { MiscellaneousDueDateEnum } from "~/app/_enum/miscellaneous-due-date.enum";
import { YesNoEnum } from "~/app/_enum/yesno-unit.enum";

export class TermMapper
  implements
    IBaseEntityMapper<Term>,
    IBaseDtoEntityMapper<Term, TermDetailsFormSource, TermDto>
{
  mapEntity(t: Term): Term {
    const term = new Term();
    const termMiscellaneousMapper = new TermMiscellaneousMapper();

    term.id = t.id;
    term.code = t.code;
    term.name = t.name;
    term.termParentId = t.termParentId;
    term.spaceTypeId = t.spaceTypeId;

    if (t.spaceType) {
      term.spaceType = new SpaceType();
      term.spaceType.name = t.spaceType.name;
      term.spaceType.isDeletable = t.spaceType.isDeletable;
      term.spaceType.isActive = t.spaceType.isActive;
    }

    term.rate = t.rate;
    term.maximumNumberOfOccupants = t.maximumNumberOfOccupants;
    term.durationUnit = t.durationUnit;
    term.durationUnitText = t.durationUnitText;
    term.advancedPaymentDurationValue = t.advancedPaymentDurationValue;
    term.depositPaymentDurationValue = t.depositPaymentDurationValue;

    term.excludeElectricBill = t.excludeElectricBill;
    term.electricBillAmount = t.electricBillAmount;
    term.excludeWaterBill = t.excludeWaterBill;
    term.waterBillAmount = t.waterBillAmount;
    term.penaltyValue = t.penaltyValue;
    term.penaltyAmount = t.penaltyAmount;
    term.penaltyValueType = t.penaltyValueType;
    term.penaltyAmountPerDurationUnit = t.penaltyAmountPerDurationUnit;
    term.generateAccountStatementDaysBeforeValue =
      t.generateAccountStatementDaysBeforeValue;
    term.penaltyAmountPerDurationUnitText = t.penaltyAmountPerDurationUnitText;
    term.penaltyEffectiveAfterDurationValue =
      t.penaltyEffectiveAfterDurationValue;
    term.penaltyEffectiveAfterDurationUnit =
      t.penaltyEffectiveAfterDurationUnit;
    term.penaltyEffectiveAfterDurationUnitText =
      t.penaltyEffectiveAfterDurationUnitText;
    term.miscellaneousDueDate = t.miscellaneousDueDate;
    term.includeMiscellaneousCheckAndCalculateForPenalty =
      t.includeMiscellaneousCheckAndCalculateForPenalty;
    term.autoSendNewAccountStatement = t.autoSendNewAccountStatement;
    term.isActive = t.isActive;
    term.isDeletable = t.isDeletable;

    if (t.termMiscellaneous && t.termMiscellaneous.length > 0) {
      term.termMiscellaneous = t.termMiscellaneous.map(
        (tm: TermMiscellaneous) => {
          return termMiscellaneousMapper.mapEntity(tm);
        }
      );
    }

    return term;
  }

  initFormSource() {
    const termFormSource = new TermDetailsFormSource(
      "",
      "",
      0,
      0.0,
      0,
      DurationEnum.Month,
      1,
      2,
      YesNoEnum.Yes,
      0.0,
      YesNoEnum.Yes,
      0.0,
      0.0,
      ValueTypeEnum.Fixed,
      DurationEnum.Day,
      0,
      DurationEnum.Day,
      5,
      YesNoEnum.No,
      MiscellaneousDueDateEnum.SameWithRentalDueDate,
      YesNoEnum.No
    );

    return termFormSource;
  }

  mapFormSourceToDto(termFormSource: TermDetailsFormSource): TermDto {
    const term = new TermDto(
      termFormSource.code,
      termFormSource.name,
      termFormSource.spaceTypeId,
      termFormSource.rate,
      termFormSource.maximumNumberOfOccupants,
      termFormSource.durationUnit,
      termFormSource.advancedPaymentDurationValue,
      termFormSource.depositPaymentDurationValue,
      termFormSource.excludeElectricBill,
      termFormSource.electricBillAmount,
      termFormSource.excludeWaterBill,
      termFormSource.waterBillAmount,
      termFormSource.penaltyValue,
      termFormSource.penaltyValueType,
      termFormSource.penaltyAmountPerDurationUnit,
      termFormSource.penaltyEffectiveAfterDurationValue,
      termFormSource.penaltyEffectiveAfterDurationUnit,
      termFormSource.generateAccountStatementDaysBeforeValue,
      termFormSource.autoSendNewAccountStatement,
      termFormSource.miscellaneousDueDate,
      termFormSource.includeMiscellaneousCheckAndCalculateForPenalty
    );
    return term;
  }

  mapEntityToFormSource(term: Term): TermDetailsFormSource {
    const termFormSource = new TermDetailsFormSource(
      term.code,
      term.name,
      term.spaceTypeId,
      term.rate,
      term.maximumNumberOfOccupants,
      term.durationUnit,
      term.advancedPaymentDurationValue,
      term.depositPaymentDurationValue,
      term.excludeElectricBill ? YesNoEnum.Yes : YesNoEnum.No,
      term.electricBillAmount,
      term.excludeWaterBill ? YesNoEnum.Yes : YesNoEnum.No,
      term.waterBillAmount,
      term.penaltyValue,
      term.penaltyValueType,
      term.penaltyAmountPerDurationUnit,
      term.penaltyEffectiveAfterDurationValue,
      term.penaltyEffectiveAfterDurationUnit,
      term.generateAccountStatementDaysBeforeValue,
      term.autoSendNewAccountStatement ? YesNoEnum.Yes : YesNoEnum.No,
      term.miscellaneousDueDate,
      term.includeMiscellaneousCheckAndCalculateForPenalty
        ? YesNoEnum.Yes
        : YesNoEnum.No
    );

    return termFormSource;
  }

  mapFormSourceToEntity(formSource: TermDetailsFormSource): Term {
    const term = new Term();
    term.code = formSource.code;
    term.name = formSource.name;
    term.spaceTypeId = formSource.spaceTypeId;
    term.rate = formSource.rate;
    term.maximumNumberOfOccupants = formSource.maximumNumberOfOccupants;
    term.durationUnit = formSource.durationUnit;
    term.advancedPaymentDurationValue = formSource.advancedPaymentDurationValue;
    term.depositPaymentDurationValue = formSource.depositPaymentDurationValue;
    term.excludeElectricBill = formSource.excludeElectricBill === YesNoEnum.Yes;
    term.electricBillAmount = formSource.electricBillAmount;
    term.excludeWaterBill = formSource.excludeWaterBill === YesNoEnum.Yes;
    term.waterBillAmount = formSource.waterBillAmount;
    term.penaltyValue = formSource.penaltyValue;
    term.penaltyValueType = formSource.penaltyValueType;
    term.penaltyAmountPerDurationUnit = formSource.penaltyAmountPerDurationUnit;
    term.penaltyEffectiveAfterDurationValue =
      formSource.penaltyEffectiveAfterDurationValue;
    term.penaltyEffectiveAfterDurationUnit =
      formSource.penaltyEffectiveAfterDurationUnit;
    term.generateAccountStatementDaysBeforeValue =
      formSource.generateAccountStatementDaysBeforeValue;
    term.autoSendNewAccountStatement =
      formSource.autoSendNewAccountStatement === YesNoEnum.Yes;
    term.miscellaneousDueDate = formSource.miscellaneousDueDate;
    term.includeMiscellaneousCheckAndCalculateForPenalty =
      formSource.includeMiscellaneousCheckAndCalculateForPenalty ===
      YesNoEnum.Yes;

    return term;
  }
}
