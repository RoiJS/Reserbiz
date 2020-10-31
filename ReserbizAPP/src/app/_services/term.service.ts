import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { Term } from '../_models/term.model';
import { BaseService } from './base.service';
import { TermMapper } from '../_helpers/term-mapper.helper';
import { IBaseService } from '../_interfaces/ibase-service.interface';
import { IEntityFilter } from '../_interfaces/ientity-filter.interface';
import { IDtoProcess } from '../_interfaces/idto-process.interface';
import { TermMiscellaneous } from '../_models/term-miscellaneous.model';
import { TranslateService } from '@ngx-translate/core';
import { TermOption } from '../_models/term-option.model';

@Injectable({ providedIn: 'root' })
export class TermService
  extends BaseService<Term>
  implements IBaseService<Term> {
  private _loadTermListFlag = new BehaviorSubject<void>(null);

  constructor(public http: HttpClient) {
    super(new TermMapper(), http);
  }

  getEntities(entityFilter: IEntityFilter): Observable<Term[]> {
    const searchKeyword = entityFilter.searchKeyword || '';
    return this.getEntitiesFromServer(
      `${this._apiBaseUrl}/term?termKeywords=${searchKeyword}`
    );
  }

  async getTerm(termId: number): Promise<Term> {
    return await this.getEntityFromServer(
      `${this._apiBaseUrl}/term/${termId}`
    ).toPromise();
  }

  getTermsAsOptions(
    translateService: TranslateService
  ): Observable<TermOption[]> {
    return this.http
      .get<TermOption[]>(`${this._apiBaseUrl}/term/getTermsAsOptions`)
      .pipe(
        map((stos: TermOption[]) => {
          return stos.map((st: TermOption) => {
            const spaceTypeOption = new TermOption();
            spaceTypeOption.id = st.id;
            spaceTypeOption.name = st.name;
            spaceTypeOption.isDelete = st.isDelete;
            spaceTypeOption.isActive = st.isActive;
            spaceTypeOption.canBeSelected = st.canBeSelected;
            spaceTypeOption.inactiveText = translateService.instant(
              'GENERAL_TEXTS.INACTIVE'
            );
            return spaceTypeOption;
          });
        })
      );
  }

  deleteMultipleItems(terms: Term[]): Observable<void> {
    return this.deleteMultipleItemsOnServer(
      `${this._apiBaseUrl}/term/deleteMultipleTerms`,
      terms
    );
  }

  deleteItem(termId: number): Observable<void> {
    return this.deleteItemOnServer(`${this._apiBaseUrl}/term/${termId}`);
  }

  setEntityStatus(termId: number, status: boolean): Observable<void> {
    return this.setEntityStatusOnServer(
      `${this._apiBaseUrl}/term/setStatus/${termId}/${status}`
    );
  }

  updateEntity(termForUpdateDto: IDtoProcess): Observable<void> {
    return this.updateEntityToServer(
      `${this._apiBaseUrl}/term/${termForUpdateDto.id}`,
      termForUpdateDto.dtoEntity
    );
  }

  saveNewTerm(
    newTerm: Term,
    newTermMiscellaneous: TermMiscellaneous[]
  ): Observable<void> {
    const termCreateDto = new Term();
    termCreateDto.code = newTerm.code;
    termCreateDto.name = newTerm.name;
    termCreateDto.spaceTypeId = newTerm.spaceTypeId;
    termCreateDto.rate = newTerm.rate;
    termCreateDto.maximumNumberOfOccupants = newTerm.maximumNumberOfOccupants;
    termCreateDto.durationUnit = newTerm.durationUnit;
    termCreateDto.advancedPaymentDurationValue =
      newTerm.advancedPaymentDurationValue;
    termCreateDto.depositPaymentDurationValue =
      newTerm.depositPaymentDurationValue;
    termCreateDto.excludeElectricBill = newTerm.excludeElectricBill;
    termCreateDto.electricBillAmount = newTerm.electricBillAmount;
    termCreateDto.excludeWaterBill = newTerm.excludeWaterBill;
    termCreateDto.waterBillAmount = newTerm.waterBillAmount;
    termCreateDto.penaltyValue = newTerm.penaltyValue;
    termCreateDto.penaltyValueType = newTerm.penaltyValueType;
    termCreateDto.penaltyAmountPerDurationUnit =
      newTerm.penaltyAmountPerDurationUnit;
    termCreateDto.penaltyEffectiveAfterDurationValue =
      newTerm.penaltyEffectiveAfterDurationValue;
    termCreateDto.penaltyEffectiveAfterDurationUnit =
      newTerm.penaltyEffectiveAfterDurationUnit;

    termCreateDto.termMiscellaneous = newTermMiscellaneous.map(
      (tm: TermMiscellaneous) => {
        const termMiscellaneous = new TermMiscellaneous();
        termMiscellaneous.name = tm.name;
        termMiscellaneous.description = tm.description;
        termMiscellaneous.amount = tm.amount;
        return termMiscellaneous;
      }
    );

    return this.http.post<void>(
      `${this._apiBaseUrl}/term/create`,
      termCreateDto
    );
  }

  async checkTermCodeIfExists(
    termId: number,
    termCode: string
  ): Promise<boolean> {
    return await this.http
      .get<boolean>(
        `${this._apiBaseUrl}/term/checkTermCodeIfExists/${termId}/${termCode}`
      )
      .toPromise();
  }

  async checkSpaceTypeAvailability(termId: number): Promise<boolean> {
    return this.http
      .get<boolean>(
        `${this._apiBaseUrl}/term/checkTermSpaceTypeAvailability/${termId}`
      )
      .toPromise();
  }

  reloadListFlag() {
    this._loadTermListFlag.next();
  }

  get loadTermListFlag(): BehaviorSubject<void> {
    return this._loadTermListFlag;
  }
}
