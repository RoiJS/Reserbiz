import { IEntity } from '../../_interfaces/ientity.interface';
import { IPaymentPaginationList } from '../../_interfaces/pagination_list/ipayment-pagination-list.interface';

export class PaymentPaginationList implements IPaymentPaginationList {
  public totalAmount: number;
  public suggestedAmountForPayment: number;
  public depositedAmountBalance: number;
  public totalAmountFromDeposit: number;
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