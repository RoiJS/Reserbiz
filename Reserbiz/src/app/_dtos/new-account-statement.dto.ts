import { AccountStatementTypeEnum } from '../_enum/account-statement-type.enum';

import { IBaseDto } from '../_interfaces/ibase-dto.interface';

export class NewAccountStatementDto implements IBaseDto {
  public contractId: number;
  public dueDate: Date;
  public electricBill: number;
  public waterBill: number;
  public accountStatementType: AccountStatementTypeEnum;

  constructor() {
    this.contractId = 0;
    this.dueDate = new Date();
    this.electricBill = 0;
    this.waterBill = 0;
  }
}
