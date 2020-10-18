import { IBaseDto } from '../_interfaces/ibase-dto.interface';

export class AccountStatementDto implements IBaseDto {
  constructor(
    public electricBill: number,
    public waterBill: number
  ) {}
}
