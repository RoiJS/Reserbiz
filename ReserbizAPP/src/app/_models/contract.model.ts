import { TranslateService } from '@ngx-translate/core';

import { Entity } from './entity.model';
import { ContractDurationBeforeContractEnds } from './contract-duration-before-contract-ends.model';

import { DurationEnum } from '../_enum/duration-unit.enum';
import { DateFormatter } from '../_helpers/formatters/date-formatter.helper';

export class Contract extends Entity {
  public code: string;
  public tenantId: number;
  public termId: number;
  public spaceId: number;
  public spaceName: string;
  public termParentId: number;
  public tenantName: string;
  public effectiveDate: Date;
  public isOpenContract: boolean;
  public durationValue: number;
  public durationUnit: DurationEnum;
  public expirationDate: Date;
  public isExpired: boolean;
  public isEditable: boolean;

  public nextDueDate: Date;
  public contractDurationBeforeContractEnds: ContractDurationBeforeContractEnds[];
  public contractDurationBeforeContractEndsText: string;
  public spaceTypeName: string;
  public spaceTypeId: number;
  public accountStatementsCount: number;

  constructor() {
    super();
    this.id = 0;
    this.code = '';
    this.tenantId = 0;
    this.termId = 0;
    this.spaceId = 0;
    this.spaceName = '';
    this.effectiveDate = null;
    this.isOpenContract = false;
    this.durationValue = 0;
    this.durationUnit = DurationEnum.None;
    this.expirationDate = null;
    this.isExpired = false;
    this.isEditable = true;
    this.nextDueDate = null;
    this.contractDurationBeforeContractEnds = [];
    this.contractDurationBeforeContractEndsText = '';
    this.spaceTypeName = '';
    this.spaceTypeId = 0;
    this.accountStatementsCount = 0;
  }

  public convertDurationBeforeContractEndsToString(
    translateService: TranslateService
  ): void {
    let counter = 0;
    this.contractDurationBeforeContractEndsText = '';

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

  get backgroundColor(): string {
    const randomIndex = this.getNumberFirstDigit();
    // Get color randomly from the list
    return this.colorList[randomIndex];
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

  private getNumberFirstDigit(): number {
    const stringId = this.id.toString().split('');
    return +stringId[stringId.length - 1];
  }

  get hasAccountStatements(): boolean {
    return this.accountStatementsCount > 0;
  }

  get nextDueDateMonthName(): string {
    if (!this.nextDueDate) {
      return '';
    }

    const months = [
      'Jan',
      'Feb',
      'Mar',
      'Apr',
      'May',
      'Jun',
      'Jul',
      'Aug',
      'Sep',
      'Oct',
      'Nov',
      'Dec',
    ];

    const monthName = months[this.nextDueDate.getMonth()];
    return monthName;
  }

  get nextDueDateDayName(): string {
    if (!this.nextDueDate) {
      return '';
    }

    const days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];

    const dayName = days[this.nextDueDate.getDay()];
    return dayName;
  }

  get nextDueDateDay(): number {
    if (!this.nextDueDate) {
      return 0;
    }
    return this.nextDueDate.getDate();
  }

  get nextDueDateYear(): number {
    if (!this.nextDueDate) {
      return 0;
    }
    return this.nextDueDate.getFullYear();
  }

  get isNextDueDateYearCurrentYear(): boolean {
    const result = Boolean(
      this.nextDueDate.getFullYear() === new Date().getFullYear()
    );
    return result;
  }

  get effectiveDateFormatted(): string {
    return DateFormatter.format(this.effectiveDate, 'MMM DD, YYYY');
  }

  get nextDueDateFormatted(): string {
    return DateFormatter.format(this.nextDueDate, 'MMM DD, YYYY');
  }

  get expirationDateFormatted(): string {
    return DateFormatter.format(this.expirationDate, 'MMM DD, YYYY');
  }
}
