import { IAccountStatementPaginationList } from '../../_interfaces/pagination_list/iaccount-statement-pagination-list.interface';
import { IEntity } from '../../_interfaces/ientity.interface';

export class AccountStatementPaginationList
  implements IAccountStatementPaginationList {
  public totalExpectedAmount: number;
  public totalPaidAmount: number;
  public totalExpectedDepositAmount: number;
  public totalPaidAmountFromDeposit: number;
  public totalEncashedDepositedAmount: number;
  public totalItems: number;
  public page: number;
  public numberOfItemsPerPage: number;
  public items: IEntity[];

  constructor() {
    this.totalExpectedAmount = 0;
    this.totalPaidAmount = 0;
    this.totalItems = 0;
    this.page = 0;
    this.numberOfItemsPerPage = 0;
    this.items = [];
  }
}
