import { Injectable } from '@angular/core';

import { Term } from '../_models/term.model';
import { EntityService } from './entity.service';

@Injectable({ providedIn: 'root' })
export class LocalManageTermService extends EntityService<Term> {
  constructor() {
    super(Term);
  }

  isSame(otherTermDetails: Term): boolean {
    const currentTermDetails = this.entityDetails.value;
    const hasNotChange = Boolean(
      currentTermDetails.spaceTypeId === otherTermDetails.spaceTypeId &&
        currentTermDetails.rate === otherTermDetails.rate &&
        currentTermDetails.maximumNumberOfOccupants ===
          otherTermDetails.maximumNumberOfOccupants &&
        currentTermDetails.durationUnit === otherTermDetails.durationUnit &&
        currentTermDetails.advancedPaymentDurationValue ===
          otherTermDetails.advancedPaymentDurationValue &&
        currentTermDetails.depositPaymentDurationValue ===
          otherTermDetails.depositPaymentDurationValue &&
        currentTermDetails.excludeElectricBill ===
          otherTermDetails.excludeElectricBill &&
        currentTermDetails.electricBillAmount ===
          otherTermDetails.electricBillAmount &&
        currentTermDetails.excludeWaterBill ===
          otherTermDetails.excludeWaterBill &&
        currentTermDetails.waterBillAmount ===
          otherTermDetails.waterBillAmount &&
        currentTermDetails.penaltyValue === otherTermDetails.penaltyValue &&
        currentTermDetails.penaltyValueType ===
          otherTermDetails.penaltyValueType &&
        currentTermDetails.penaltyAmountPerDurationUnit ===
          otherTermDetails.penaltyAmountPerDurationUnit &&
        currentTermDetails.penaltyEffectiveAfterDurationValue ===
          otherTermDetails.penaltyEffectiveAfterDurationValue &&
        currentTermDetails.penaltyEffectiveAfterDurationUnit ===
          otherTermDetails.penaltyEffectiveAfterDurationUnit
    );

    return hasNotChange;
  }
}
