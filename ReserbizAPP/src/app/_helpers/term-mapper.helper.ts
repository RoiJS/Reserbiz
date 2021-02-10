import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';

import { TermMiscellaneousMapper } from './term-miscellaneous-mapper.helper';
import { Term } from '../_models/term.model';
import { TermMiscellaneous } from '../_models/term-miscellaneous.model';
import { IBaseDtoEntityMapper } from '../_interfaces/ibase-dto-entity-mapper.interface';
import { TermDetailsFormSource } from '../_models/term-details-form.model';
import { SpaceType } from '../_models/space-type.model';
import { TermDto } from '../_dtos/term.dto';
import { DurationEnum } from '../_enum/duration-unit.enum';
import { ValueTypeEnum } from '../_enum/value-type.enum';

export class TermMapper
  implements
    IBaseEntityMapper<Term>,
    IBaseDtoEntityMapper<Term, TermDetailsFormSource, TermDto> {
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
    term.penaltyAmountPerDurationUnitText = t.penaltyAmountPerDurationUnitText;
    term.penaltyEffectiveAfterDurationValue =
      t.penaltyEffectiveAfterDurationValue;
    term.penaltyEffectiveAfterDurationUnit =
      t.penaltyEffectiveAfterDurationUnit;
    term.penaltyEffectiveAfterDurationUnitText =
      t.penaltyEffectiveAfterDurationUnitText;
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
      '',
      '',
      0,
      0.0,
      0,
      DurationEnum.Month,
      0,
      0,
      true,
      0.0,
      true,
      0.0,
      0.0,
      ValueTypeEnum.Fixed,
      DurationEnum.Day,
      0,
      DurationEnum.Day
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
      termFormSource.penaltyEffectiveAfterDurationUnit
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
      term.excludeElectricBill,
      term.electricBillAmount,
      term.excludeWaterBill,
      term.waterBillAmount,
      term.penaltyValue,
      term.penaltyValueType,
      term.penaltyAmountPerDurationUnit,
      term.penaltyEffectiveAfterDurationValue,
      term.penaltyEffectiveAfterDurationUnit
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
    term.excludeElectricBill = formSource.excludeElectricBill;
    term.electricBillAmount = formSource.electricBillAmount;
    term.excludeWaterBill = formSource.excludeWaterBill;
    term.waterBillAmount = formSource.waterBillAmount;
    term.penaltyValue = formSource.penaltyValue;
    term.penaltyValueType = formSource.penaltyValueType;
    term.penaltyAmountPerDurationUnit = formSource.penaltyAmountPerDurationUnit;
    term.penaltyEffectiveAfterDurationValue =
      formSource.penaltyEffectiveAfterDurationValue;
    term.penaltyEffectiveAfterDurationUnit =
      formSource.penaltyEffectiveAfterDurationUnit;

    return term;
  }
}
