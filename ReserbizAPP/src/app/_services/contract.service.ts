import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, BehaviorSubject, ObservableLike } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';

import { BaseService } from './base.service';

import { Contract } from '../_models/contract.model';
import { ContractPaginationList } from '../_models/contract-pagination-list.model';
import { EntityPaginationList } from '../_models/entity-pagination-list.model';
import { Term } from '../_models/term.model';
import { TermMiscellaneous } from '../_models/term-miscellaneous.model';

import { ContractMapper } from '../_helpers/contract-mapper.helper';

import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IContractFilter } from '../_interfaces/icontract-filter.interface';

import { DurationEnum } from '../_enum/duration-unit.enum';
import { ContractDto } from '../_dtos/contract-dto';

@Injectable({ providedIn: 'root' })
export class ContractService
  extends BaseService<Contract>
  implements IBaseService<Contract> {
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

  async saveNewContract(
    contractDetails: Contract,
    termDetails: Term,
    termMiscellaneousList: TermMiscellaneous[]
  ): Promise<void> {
    const contractCreateDto = new ContractDto(
      contractDetails.code,
      contractDetails.tenantId,
      contractDetails.termId,
      contractDetails.effectiveDate,
      contractDetails.isOpenContract,
      contractDetails.durationUnit,
      contractDetails.durationValue
    );

    contractCreateDto.term.code = termDetails.code;
    contractCreateDto.term.name = termDetails.name;
    contractCreateDto.term.spaceTypeId = termDetails.spaceTypeId;
    contractCreateDto.term.rate = termDetails.rate;
    contractCreateDto.term.maximumNumberOfOccupants =
      termDetails.maximumNumberOfOccupants;
    contractCreateDto.term.durationUnit = termDetails.durationUnit;
    contractCreateDto.term.advancedPaymentDurationValue =
      termDetails.advancedPaymentDurationValue;
    contractCreateDto.term.depositPaymentDurationValue =
      termDetails.depositPaymentDurationValue;
    contractCreateDto.term.excludeElectricBill =
      termDetails.excludeElectricBill;
    contractCreateDto.term.electricBillAmount = termDetails.electricBillAmount;
    contractCreateDto.term.excludeWaterBill = termDetails.excludeWaterBill;
    contractCreateDto.term.waterBillAmount = termDetails.waterBillAmount;
    contractCreateDto.term.penaltyValue = termDetails.penaltyValue;
    contractCreateDto.term.penaltyValueType = termDetails.penaltyValueType;
    contractCreateDto.term.penaltyAmountPerDurationUnit =
      termDetails.penaltyAmountPerDurationUnit;
    contractCreateDto.term.penaltyEffectiveAfterDurationValue =
      termDetails.penaltyEffectiveAfterDurationValue;
    contractCreateDto.term.penaltyEffectiveAfterDurationUnit =
      termDetails.penaltyEffectiveAfterDurationUnit;

    contractCreateDto.term.termMiscellaneous = termMiscellaneousList.map(
      (tm: TermMiscellaneous) => {
        const termMiscellaneous = new TermMiscellaneous();
        termMiscellaneous.name = tm.name;
        termMiscellaneous.description = tm.description;
        return termMiscellaneous;
      }
    );

    return this.http
      .post<void>(`${this._apiBaseUrl}/contract/create`, contractCreateDto)
      .toPromise();
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

  get loadContractListFlag(): BehaviorSubject<void> {
    return this._loadContractListFlag;
  }
}
