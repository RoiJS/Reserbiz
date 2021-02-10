import { IContractPaginationList } from '../_interfaces/icontract-pagination-list.interface';
import { IEntity } from '../_interfaces/ientity.interface';

export class ContractPaginationList implements IContractPaginationList {
  public totalNumberOfOpenContracts: number;
  public totalNumberOfInactiveContracts: number;
  public totalNumberOfExpiredContracts: number;
  public totalItems: number;
  public page: number;
  public numberOfItemsPerPage: number;
  public items: IEntity[];

  constructor() {
    this.totalNumberOfOpenContracts = 0;
    this.totalItems = 0;
    this.page = 0;
    this.numberOfItemsPerPage = 0;
    this.items = [];
  }
}
