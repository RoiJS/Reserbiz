import { TranslateService } from '@ngx-translate/core';

import { Entity } from './entity.model';
import { ContractDurationBeforeContractEnds } from './contract-duration-before-contract-ends.model';

import { DurationEnum } from '../_enum/duration-unit.enum';

export class Contract extends Entity {
  public code: string;
  public tenantId: number;
  public termId: number;
  public tenantName: string;
  public effectiveDate: Date;
  public isOpenContract: boolean;
  public durationValue: number;
  public durationUnit: DurationEnum;
  public expirationDate: Date;
  public isExpired: boolean;

  public nextDueDate: Date;
  public contractDurationBeforeContractEnds: ContractDurationBeforeContractEnds[];
  public contractDurationBeforeContractEndsText: string;

  constructor() {
    super();
    this.id = 0;
    this.code = '';
    this.tenantId = 0;
    this.termId = 0;
    this.effectiveDate = null;
    this.isOpenContract = false;
    this.durationValue = 0;
    this.durationUnit = DurationEnum.None;
    this.expirationDate = null;
    this.isExpired = false;
    this.contractDurationBeforeContractEnds = [];
    this.contractDurationBeforeContractEndsText = '';
  }

  public convertDurationBeforeContractEndsToString(
    translateService: TranslateService
  ): void {
    let counter = 0;
    this.contractDurationBeforeContractEnds.forEach(
      (c: ContractDurationBeforeContractEnds) => {
        const value = c.durationValue;
        const unitName = this.getDurationUnitText(
          c.durationValue,
          c.durationUnitText,
          translateService
        );
        this.contractDurationBeforeContractEndsText += `${value} ${unitName} `;

        // Add 'and' text
        if (counter === this.contractDurationBeforeContractEnds.length - 2) {
          this.contractDurationBeforeContractEndsText += `${translateService.instant(
            'GENERAL_TEXTS.AND'
          )} `;
        }

        counter++;
      }
    );

    this.contractDurationBeforeContractEndsText = this.contractDurationBeforeContractEndsText.trim();
  }

  private getDurationUnitText(
    durationValue: number,
    durationUnitText: string,
    translateService: TranslateService
  ): string {
    let durationName = '';
    durationUnitText = durationUnitText.toUpperCase();

    durationName =
      durationValue > 1
        ? translateService.instant(
            `GENERAL_TEXTS.DURATION.${durationUnitText}.S_FORM`
          )
        : translateService.instant(
            `GENERAL_TEXTS.DURATION.${durationUnitText}.BASE_FORM`
          );

    return durationName;
  }
}
