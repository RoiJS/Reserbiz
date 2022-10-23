import { IContractDurationBeforeContractEnds } from '../_interfaces/icontract-duration-before-contract-ends.interface';

export class ContractDurationBeforeContractEnds
  implements IContractDurationBeforeContractEnds {
  durationValue: number;
  durationUnitText: string;
}
