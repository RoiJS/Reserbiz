import { IBaseFormSource } from '../../_interfaces/ibase-form-source.interface';
import { BaseForm } from './base-form.model';
import { DurationEnum } from '../../_enum/duration-unit.enum';

export class ContractDetailsFormSource
  extends BaseForm<ContractDetailsFormSource>
  implements IBaseFormSource<ContractDetailsFormSource>
{
  constructor(
    public code: string,
    public tenantId: number,
    public termId: number,
    public spaceTypeName: string,
    public spaceId: number,
    public effectiveDate: Date,
    public isOpenContract: boolean,
    public durationValue: number,
    public durationUnit: DurationEnum,
    public includeRentalFee: boolean,
    public includeUtilityBills: boolean,
    public includeMiscellaneousFees: boolean,
    public includePenaltyAmount: boolean
  ) {
    super();
  }

  clone() {
    return new ContractDetailsFormSource(
      this.code,
      this.tenantId,
      this.termId,
      this.spaceTypeName,
      this.spaceId,
      this.effectiveDate,
      this.isOpenContract,
      this.durationValue,
      this.durationUnit,
      this.includeRentalFee,
      this.includeUtilityBills,
      this.includeMiscellaneousFees,
      this.includePenaltyAmount
    );
  }
}
