import { SettingsDto } from '../../_dtos/settings-dto';
import { IBaseDtoEntityMapper } from '../../_interfaces/mappers/ibase-dto-entity-mapper.interface';

import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';

import { SettingsFormSource } from '../../_models/form/settings-form.model';
import { Settings } from '../../_models/settings.model';

export class SettingsMapper
  implements
    IBaseEntityMapper<Settings>,
    IBaseDtoEntityMapper<Settings, SettingsFormSource, SettingsDto> {
  mapEntity(settingsFromServer: Settings): Settings {
    const mappedSettings = new Settings();

    mappedSettings.businessName =
      settingsFromServer.businessName;
    return mappedSettings;
  }

  initFormSource(): SettingsFormSource {
    throw new Error('initFormSource function is not implemented!');
  }

  mapFormSourceToDto(settingsFormSource: SettingsFormSource): SettingsDto {
    return new SettingsDto(
      settingsFormSource.businessName
    );
  }

  mapEntityToFormSource(settings: Settings): SettingsFormSource {
    return new SettingsFormSource(
      settings.businessName
    );
  }

  mapFormSourceToEntity(settingsFormSource: SettingsFormSource): Settings {
    const settings = new Settings();
    settings.businessName =
      settingsFormSource.businessName;
    return settings;
  }
}
