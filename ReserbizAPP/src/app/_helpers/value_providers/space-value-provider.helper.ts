import { TranslateService } from '@ngx-translate/core';
import { take } from 'rxjs/operators';

import { ISpaceValueProvider } from '../../_interfaces/value_providers/ispace-value-provider.interface';

import { SpaceOption } from '../../_models/options/space-option.model';

import { SpaceService } from '../../_services/space.service';

export class SpaceValueProvider implements ISpaceValueProvider {
  private _spaceTypesOptions: SpaceOption[] = [];
  private _currentValue = 0;
  private _currentSpaceTypeId: number;

  constructor(
    private translateService: TranslateService,
    private spaceService: SpaceService
  ) {
    this.spaceService
      .getSpacesAsOptions(this.translateService)
      .pipe(take(1))
      .subscribe((spaceOptions: SpaceOption[]) => {
        // Define a default option
        const defaultSpaceOption = new SpaceOption();
        defaultSpaceOption.id = 0;
        defaultSpaceOption.name = '';
        defaultSpaceOption.spaceTypeId = 0;
        defaultSpaceOption.isActive = true;
        defaultSpaceOption.isDelete = false;
        defaultSpaceOption.canBeSelected = true;

        this._spaceTypesOptions.push(defaultSpaceOption);
        this._spaceTypesOptions.push(...spaceOptions);
      });
  }

  getItem(itemId: number): SpaceOption {
    return this.spaceOptions.items.find((s: SpaceOption) => s.id === itemId);
  }

  setCurrenValue(value) {
    this._currentValue = value;
  }

  setCurrentSpaceTypeId(id: number) {
    this._currentSpaceTypeId = id;
  }

  get spaceOptions(): {
    key: string;
    label: string;
    items: SpaceOption[];
  } {
    // Filter options that are not inactive or the current value
    // and filtered by current spaceTypeId
    const _spaceOptions = this._spaceTypesOptions.filter(
      (sp) =>
        (sp.canBeSelected || sp.id === this._currentValue) &&
        (sp.spaceTypeId === this._currentSpaceTypeId || sp.spaceTypeId === 0)
    );

    return {
      key: 'id',
      label: 'displayName',
      items: _spaceOptions,
    };
  }
}
