export class Contract {
  public id: number;
  public code: string;
  public tenantId: number;
  public termId: number;
  public effectiveDate: Date;
  public isOpenContract: boolean;
  public durationValue: number;
  public status: boolean;
  public expirationDate: Date;
  public isExpired: boolean;
  constructor() {
    this.id = 0;
    this.code = '';
    this.tenantId = 0;
    this.termId = 0;
    this.effectiveDate = null;
    this.isOpenContract = false;
    this.durationValue = 0;
    this.status = false;
    this.expirationDate = null;
    this.isExpired = false;
  }
}
