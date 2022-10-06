import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';
import { GeneralInformation } from '../../_models/general-information.model';

export class GeneralInformationMapper
  implements IBaseEntityMapper<GeneralInformation> {
  mapEntity(
    generalInformationFromServer: GeneralInformation
  ): GeneralInformation {
    const mappedSettings = new GeneralInformation();

    mappedSettings.systemUpdateStatus =
      generalInformationFromServer.systemUpdateStatus;
    return mappedSettings;
  }
}
