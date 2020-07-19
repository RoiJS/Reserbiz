export class ContractDto {
  constructor(
    public code: string,
    public tenantId: number,
    public termId: number,
    public effectiveDate: Date,
    public isOpenContract: boolean,
    public durationValue: number,
  ) {}
}
