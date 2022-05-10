import { ArchivedContractStatusEnum } from '../../_enum/archived-contract-options.enum';

export interface IArchivedContractStatusValueProvider {
    statusOptions: Array<{ key: ArchivedContractStatusEnum; label: string }>;
}
