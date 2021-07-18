import { ArchivedContractStatusEnum } from '@src/app/_enum/archived-contract-options.enum';

export interface IArchivedContractStatusValueProvider {
    statusOptions: Array<{ key: ArchivedContractStatusEnum; label: string }>;
}
