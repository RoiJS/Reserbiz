import { IEntity } from '../../_interfaces/ientity.interface';
import { IPaymentPaginationList } from '../../_interfaces/pagination_list/ipayment-pagination-list.interface';

export class PaymentPaginationList implements IPaymentPaginationList {
  public totalAmount: number;
  public suggestedRentalAmount: number;
  public suggestedElectricBillAmount: number;
  public suggestedWaterBillAmount: number;
  public suggestedMiscelleneousAmount: number;
  public suggestedPenaltyAmount: number;
  public depositedAmountBalance: number;
  public totalAmountFromDeposit: number;

  public totalExpectedRentalAmount: number;
  public totalExpectedElectricBillAmount: number;
  public totalExpectedWaterBillAmount: number;
  public totalExpectedMiscellaneousFeesAmount: number;
  public totalExpectedPenaltyAmount: number;

  public totalPaidRentalAmount: number;
  public totalPaidElectricBillAmount: number;
  public totalPaidWaterBillAmount: number;
  public totalPaidMiscellaneousFeesAmount: number;
  public totalPaidPenaltyAmount: number;

  public totalItems: number;
  public page: number;
  public numberOfItemsPerPage: number;
  public items: IEntity[];

  constructor() {
    this.totalAmount = 0;
    this.totalItems = 0;
    this.page = 0;
    this.numberOfItemsPerPage = 0;
    this.items = [];
  }
}
