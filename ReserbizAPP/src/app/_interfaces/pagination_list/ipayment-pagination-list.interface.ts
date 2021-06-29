import { IEntityPaginationList } from './ientity-pagination-list.interface';

export interface IPaymentPaginationList extends IEntityPaginationList {
  totalAmount: number;
  suggestedRentalAmount: number;
  suggestedElectricBillAmount: number;
  suggestedWaterBillAmount: number;
  suggestedMiscelleneousAmount: number;
  suggestedPenaltyAmount: number;
  depositedAmountBalance: number;
  totalAmountFromDeposit: number;
}
