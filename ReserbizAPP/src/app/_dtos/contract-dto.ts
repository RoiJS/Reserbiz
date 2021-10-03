import { DurationEnum } from '../_enum/duration-unit.enum';
import { Term } from '../_models/term.model';

export class ContractDto {
  public term: Term;

  constructor(
    public code: string,
    public tenantId: number,
    public termId: number,
    public spaceId: number,
    public effectiveDate: Date,
    public isOpenContract: boolean,
    public durationUnit: DurationEnum,
    public durationValue: number,
  ) {
    this.term = new Term();
    this.term.termParentId = termId;
  }
}
