import { IGenderValueProvider } from '../../_interfaces/value_providers/igender-value-provider.interface';
import { GenderEnum } from '../../_enum/gender.enum';
import { TranslateService } from '@ngx-translate/core';

export class GenderValueProvider implements IGenderValueProvider {
  constructor(private translateService: TranslateService) {}

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return [
      {
        key: GenderEnum.Male,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.MALE'),
      },
      {
        key: GenderEnum.Female,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.FEMALE'),
      },
    ];
  }
}
