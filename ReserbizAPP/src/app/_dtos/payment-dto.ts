export class PaymentDto {
  constructor(
    public dateTimeReceived: Date,
    public amount: number,
    public notes: string,
    public isAmountFromDeposit: boolean
  ) {}
}
