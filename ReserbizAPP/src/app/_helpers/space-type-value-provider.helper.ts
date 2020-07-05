import { TranslateService } from '@ngx-translate/core';
import { take } from 'rxjs/operators';

import { ISpaceTypeValueProvider } from '../_interfaces/ispace-type-value-provider.interface';
import { SpaceTypeService } from '../_services/space-type.service';
import { SpaceTypeOption } from '../_models/space-type-option.model';

export class SpaceTypeValueProvider implements ISpaceTypeValueProvider {
  private _spaceTypesOptions: SpaceTypeOption[] = [];
  private _currentValue = 0;

  constructor(
    private translateService: TranslateService,
    private spaceTypeService: SpaceTypeService
  ) {
    this.spaceTypeService
      .getSpaceTypesAsOptions(this.translateService)
      .pipe(take(1))
      .subscribe((spaceTypeOptions: SpaceTypeOption[]) => {
        // Define a default option
        const defaultSpaceTypeOption = new SpaceTypeOption();
        defaultSpaceTypeOption.id = 0;
        defaultSpaceTypeOption.name = '';
        defaultSpaceTypeOption.rate = 0;
        defaultSpaceTypeOption.isActive = true;
        defaultSpaceTypeOption.isDelete = false;
        defaultSpaceTypeOption.canBeSelected = true;

        this._spaceTypesOptions.push(defaultSpaceTypeOption);
        this._spaceTypesOptions.push(...spaceTypeOptions);
      });
  }

  getItem(itemId: number): SpaceTypeOption {
    return this.spaceTypeOptions.items.find(
      (s: SpaceTypeOption) => s.id === itemId
    );
  }

  setCurrenValue(value) {
    this._currentValue = value;
  }

  get spaceTypeOptions(): {
    key: string;
    label: string;
    items: SpaceTypeOption[];
  } {
    // Filter options that are not inactive or the current value
    const _spaceTypeOptions = this._spaceTypesOptions.filter(
      (sp) => sp.canBeSelected || sp.id === this._currentValue
    );

    return {
      key: 'id',
      label: 'displayName',
      items: _spaceTypeOptions,
    };
  }
}
