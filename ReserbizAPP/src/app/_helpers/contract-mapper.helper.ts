import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';
import { IBaseDtoEntityMapper } from '../_interfaces/ibase-dto-entity-mapper.interface';

import { Contract } from '../_models/contract.model';
import { ContractDetailsFormSource } from '../_models/contact-details-form.model';
import { ContractDurationBeforeContractEnds } from '../_models/contract-duration-before-contract-ends.model';

import { ContractDto } from '../_dtos/contract-dto';
import { DurationEnum } from '../_enum/duration-unit.enum';

export class ContractMapper
  implements
    IBaseEntityMapper<Contract>,
    IBaseDtoEntityMapper<Contract, ContractDetailsFormSource, ContractDto> {
  mapEntity(c: Contract): Contract {
    const contract = new Contract();

    contract.id = c.id;
    contract.code = c.code;
    contract.tenantId = c.tenantId;
    contract.tenantName = c.tenantName;
    contract.termId = c.termId;
    contract.effectiveDate = new Date(c.effectiveDate);
    contract.isOpenContract = c.isOpenContract;
    contract.durationValue = c.durationValue;
    contract.expirationDate = new Date(c.expirationDate);
    contract.isExpired = c.isExpired;
    contract.isDeletable = c.isDeletable;

    contract.nextDueDate = new Date(c.nextDueDate);

    if (c.contractDurationBeforeContractEnds.length > 0) {
      contract.contractDurationBeforeContractEnds = c.contractDurationBeforeContractEnds.map(
        (cd: ContractDurationBeforeContractEnds) => {
          const contractDurationBeforeContractEnds = new ContractDurationBeforeContractEnds();

          contractDurationBeforeContractEnds.durationValue = Math.round(cd.durationValue);
          contractDurationBeforeContractEnds.durationUnitText = cd.durationUnitText;

          return contractDurationBeforeContractEnds;
        }
      );
    }

    return contract;
  }

  initFormSource(): ContractDetailsFormSource {
    const contractDetailsForm = new ContractDetailsFormSource(
      '',
      0,
      0,
      new Date(),
      false,
      0,
      DurationEnum.None
    );

    return contractDetailsForm;
  }

  mapFormSourceToDto(
    contractFormSource: ContractDetailsFormSource
  ): ContractDto {
    const contractForUpdate = new ContractDto(
      contractFormSource.code,
      contractFormSource.tenantId,
      contractFormSource.termId,
      contractFormSource.effectiveDate,
      contractFormSource.isOpenContract,
      contractFormSource.durationValue
    );

    return contractForUpdate;
  }

  mapEntityToFormSource(contract: Contract): ContractDetailsFormSource {
    const contractFormSource = new ContractDetailsFormSource(
      contract.code,
      contract.tenantId,
      contract.termId,
      contract.effectiveDate,
      contract.isOpenContract,
      contract.durationValue,
      contract.durationUnit
    );

    return contractFormSource;
  }

  mapFormSourceToEntity(formSource: ContractDetailsFormSource): Contract {
    throw new Error('Not implemented');
  }
}
