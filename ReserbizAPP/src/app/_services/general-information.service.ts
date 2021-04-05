import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { IBaseService } from '../_interfaces/services/ibase-service.interface';

import { BaseService } from './base.service';

import { GeneralInformation } from '@src/app/_models/general-information.model';
import { GeneralInformationMapper } from '../_helpers/mappers/general-information-mapper.helper';

@Injectable({
  providedIn: 'root',
})
export class GeneralInformationService
  extends BaseService<GeneralInformation>
  implements IBaseService<GeneralInformation> {
  private _loadGeneralInformationFlag = new BehaviorSubject<boolean>(false);

  constructor(public http: HttpClient) {
    super(new GeneralInformationMapper(), http);
  }

  async getGeneralInformation(): Promise<GeneralInformation> {
    return await this.getEntityFromServer(
      `${this._apiBaseUrl}/generalInformation`
    ).toPromise();
  }

  reloadListFlag(reset: boolean) {
    this._loadGeneralInformationFlag.next(reset);
  }
}
