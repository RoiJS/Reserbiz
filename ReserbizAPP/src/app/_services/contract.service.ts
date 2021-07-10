import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';

import { BaseService } from './base.service';

import { Contract } from '../_models/contract.model';
import { ContractPaginationList } from '../_models/pagination_list/contract-pagination-list.model';
import { EntityPaginationList } from '../_models/pagination_list/entity-pagination-list.model';
import { Term } from '../_models/term.model';
import { TermMiscellaneous } from '../_models/term-miscellaneous.model';

import { ContractMapper } from '../_helpers/mappers/contract-mapper.helper';

import { IBaseService } from '../_interfaces/services/ibase-service.interface';
import { IContractFilter } from '../_interfaces/filters/icontract-filter.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';

import { DurationEnum } from '../_enum/duration-unit.enum';
import { ContractDto } from '../_dtos/contract-dto';

@Injectable({ providedIn: 'root' })
export class ContractService
  extends BaseService<Contract>
  implements IBaseService<Contract>
{
  private _loadContractListFlag = new BehaviorSubject<void>(null);

  constructor(public http: HttpClient) {
    super(new ContractMapper(), http);
  }

  getPaginatedEntities(
    contractFilter: IContractFilter
  ): Observable<EntityPaginationList> {
    const params = <IContractFilter>(
      this.parseRequestParams(contractFilter.toFilterJSON())
    );

    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/contract/getAllContracts`,
      params
    );
  }

  async getContract(contractId: number): Promise<Contract> {
    return await this.getEntityFromServer(
      `${this._apiBaseUrl}/contract/${contractId}`
    ).toPromise();
  }

  deleteMultipleItems(contracts: Contract[]): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/contract/deleteMultipleContracts`,
      contracts
    );
  }

  deleteItem(contractId: number): Observable<void> {
    return this.deleteItemOnServer(
      `${this._apiBaseUrl}/contract/deleteContract?contractId=${contractId}`
    );
  }

  setEntityStatus(contractId: number, status: boolean): Observable<void> {
    return this.setEntityStatusOnServer(
      `${this._apiBaseUrl}/contract/setStatus/${contractId}/${status}`
    );
  }

  setEncashDepositAmountStatus(
    contractId: number,
    status: boolean
  ): Observable<void> {
    return this.setEntityStatusOnServer(
      `${this._apiBaseUrl}/contract/setEncashDepositAmountStatus/${contractId}/${status}`
    );
  }

  updateEntity(contractForUpdateDto: IDtoProcess): Observable<void> {
    return this.updateEntityToServer(
      `${this._apiBaseUrl}/contract/${contractForUpdateDto.id}`,
      contractForUpdateDto.dtoEntity
    );
  }

  setMultipleEntityStatus(
    contracts: Contract[],
    status: boolean
  ): Observable<void> {
    return this.setMultipleEntityStatusOnServer(
      `${this._apiBaseUrl}/contract/setMultipleContractsStatus/${status}`,
      contracts,
      status
    );
  }

  async manageContract(
    contractDetails: Contract,
    termDetails: Term,
    termMiscellaneousList: TermMiscellaneous[],
    originalTermMiscellaneousList: TermMiscellaneous[] = []
  ): Promise<void> {
    const contractManageDto = this.mapContractDto(
      contractDetails,
      termDetails,
      termMiscellaneousList,
      originalTermMiscellaneousList
    );

    if (contractDetails.id === 0) {
      return this.http
        .post<void>(`${this._apiBaseUrl}/contract/create`, contractManageDto)
        .toPromise();
    } else {
      return this.http
        .put<void>(
          `${this._apiBaseUrl}/contract/${contractDetails.id}`,
          contractManageDto
        )
        .toPromise();
    }
  }

  mapContractDto(
    contractDetails: Contract,
    termDetails: Term,
    termMiscellaneousList: TermMiscellaneous[],
    originalTermMiscellaneousList: TermMiscellaneous[]
  ): ContractDto {
    const isNewContract = contractDetails.id === 0;

    const contractManageDto = new ContractDto(
      contractDetails.code,
      contractDetails.tenantId,
      contractDetails.termId,
      contractDetails.spaceId,
      contractDetails.effectiveDate,
      contractDetails.isOpenContract,
      contractDetails.durationUnit,
      contractDetails.durationValue,
      contractDetails.includeRentalFee,
      contractDetails.includeUtilityBills,
      contractDetails.includeMiscellaneousFees,
      contractDetails.includePenaltyAmount
    );

    if (!isNewContract) {
      contractManageDto.term.id = termDetails.id;
    }

    contractManageDto.term.code = termDetails.code;
    contractManageDto.term.name = termDetails.name;
    contractManageDto.term.spaceTypeId = termDetails.spaceTypeId;
    contractManageDto.term.rate = termDetails.rate;
    contractManageDto.term.maximumNumberOfOccupants =
      termDetails.maximumNumberOfOccupants;
    contractManageDto.term.durationUnit = termDetails.durationUnit;
    contractManageDto.term.advancedPaymentDurationValue =
      termDetails.advancedPaymentDurationValue;
    contractManageDto.term.depositPaymentDurationValue =
      termDetails.depositPaymentDurationValue;
    contractManageDto.term.excludeElectricBill =
      termDetails.excludeElectricBill;
    contractManageDto.term.electricBillAmount = termDetails.electricBillAmount;
    contractManageDto.term.excludeWaterBill = termDetails.excludeWaterBill;
    contractManageDto.term.waterBillAmount = termDetails.waterBillAmount;
    contractManageDto.term.penaltyValue = termDetails.penaltyValue;
    contractManageDto.term.penaltyValueType = termDetails.penaltyValueType;
    contractManageDto.term.penaltyAmountPerDurationUnit =
      termDetails.penaltyAmountPerDurationUnit;
    contractManageDto.term.penaltyEffectiveAfterDurationValue =
      termDetails.penaltyEffectiveAfterDurationValue;
    contractManageDto.term.penaltyEffectiveAfterDurationUnit =
      termDetails.penaltyEffectiveAfterDurationUnit;
    contractManageDto.term.generateAccountStatementDaysBeforeValue =
      termDetails.generateAccountStatementDaysBeforeValue;
    contractManageDto.term.miscellaneousDueDate =
      termDetails.miscellaneousDueDate;

    contractManageDto.term.termMiscellaneous = termMiscellaneousList.map(
      (tm: TermMiscellaneous) => {
        const termMiscellaneous = new TermMiscellaneous();

        if (!isNewContract && tm.id > 0) {
          termMiscellaneous.id = tm.id;
        }

        termMiscellaneous.name = tm.name;
        termMiscellaneous.description = tm.description;
        termMiscellaneous.amount = tm.amount;
        return termMiscellaneous;
      }
    );

    contractManageDto.term.deletedTermMiscellaneous = [];

    originalTermMiscellaneousList.forEach((tm: TermMiscellaneous) => {
      const termMiscellaneous = new TermMiscellaneous();

      const index = contractManageDto.term.termMiscellaneous.findIndex(
        (t) => t.id === tm.id
      );

      if (index < 0) {
        termMiscellaneous.id = tm.id;
        termMiscellaneous.name = tm.name;
        termMiscellaneous.description = tm.description;
        termMiscellaneous.amount = tm.amount;
        contractManageDto.term.deletedTermMiscellaneous.push(termMiscellaneous);
      }
    });

    return contractManageDto;
  }

  reloadListFlag() {
    this._loadContractListFlag.next();
  }

  mapPaginatedEntityData(data: ContractPaginationList): EntityPaginationList {
    const contractPaginationList = new ContractPaginationList();

    contractPaginationList.totalItems = data.totalItems;
    contractPaginationList.totalNumberOfOpenContracts =
      data.totalNumberOfOpenContracts;
    contractPaginationList.totalNumberOfExpiredContracts =
      data.totalNumberOfExpiredContracts;
    contractPaginationList.totalNumberOfInactiveContracts =
      data.totalNumberOfInactiveContracts;
    contractPaginationList.page = data.page;
    contractPaginationList.numberOfItemsPerPage = data.numberOfItemsPerPage;
    const items = data.items.map((d: Contract) => {
      return this.mapper.mapEntity(d);
    });

    contractPaginationList.items = items;

    return contractPaginationList;
  }

  async checkContractCodeIfExists(
    contractId: number,
    contractCode: string
  ): Promise<boolean> {
    return await this.http
      .get<boolean>(
        `${this._apiBaseUrl}/contract/checkContractCodeIfExists/${contractId}/${contractCode}`
      )
      .toPromise();
  }

  async validateExpirationDate(
    contractId: number,
    effectiveDate: string,
    durationUnit: DurationEnum,
    durationValue: number
  ): Promise<boolean> {
    return await this.http
      .get<boolean>(
        `${this._apiBaseUrl}/contract/validateExpirationDate/${contractId}/${effectiveDate}/${durationUnit}/${durationValue}`
      )
      .toPromise();
  }

  async calculateExpirationDate(
    effectiveDate: Date,
    durationUnit: DurationEnum,
    durationValue: number
  ): Promise<Date> {
    return await this.http
      .get<Date>(
        `${this._apiBaseUrl}/contract/calculateExpirationDate/${effectiveDate}/${durationUnit}/${durationValue}`
      )
      .pipe(
        map((expirationDate: Date) => {
          return new Date(expirationDate);
        })
      )
      .toPromise();
  }

  async getActiveContractsCount(): Promise<number> {
    return this.http
      .get<number>(`${this._apiBaseUrl}/contract/getActiveContractsCount`)
      .toPromise();
  }

  async getAccountStatementsAmountSummary(): Promise<number> {
    return this.http
      .get<number>(
        `${this._apiBaseUrl}/contract/getAccountStatementsAmountSummary`
      )
      .toPromise();
  }

  getAllUpcomingDueDateContractsPerMonth(
    month: number
  ): Observable<EntityPaginationList> {
    const params = { month };

    return this.getPaginatedEntitiesFromServer(
      `${this._apiBaseUrl}/contract/getAllUpcomingDueDateContractsPerMonth`,
      params
    );
  }

  get loadContractListFlag(): BehaviorSubject<void> {
    return this._loadContractListFlag;
  }
}
