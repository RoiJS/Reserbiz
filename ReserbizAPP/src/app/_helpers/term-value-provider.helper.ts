import { TranslateService } from '@ngx-translate/core';
import { take } from 'rxjs/operators';

import { ITermValueProvider } from '../_interfaces/iterm-value-provider.interface';
import { TermService } from '../_services/term.service';
import { TermOption } from '../_models/term-option.model';

export class TermValueProvider implements ITermValueProvider {
  private _termsOptions: TermOption[] = [];
  private _currentValue = 0;

  constructor(
    private translateService: TranslateService,
    private termService: TermService
  ) {
    this.termService
      .getTermsAsOptions(this.translateService)
      .pipe(take(1))
      .subscribe((termOptions: TermOption[]) => {
        // Define a default option
        const defaultTermOption = new TermOption();
        defaultTermOption.id = 0;
        defaultTermOption.name = '';
        defaultTermOption.isActive = true;
        defaultTermOption.isDelete = false;
        defaultTermOption.canBeSelected = true;

        this._termsOptions.push(defaultTermOption);
        this._termsOptions.push(...termOptions);
      });
  }

  getItem(itemId: number): TermOption {
    return this.termOptions.items.find((s: TermOption) => s.id === itemId);
  }

  setCurrenValue(value) {
    this._currentValue = value;
  }

  get termOptions(): {
    key: string;
    label: string;
    items: TermOption[];
  } {
    // Filter options that are not inactive or the current value
    const _termOptions = this._termsOptions.filter(
      (sp) => sp.canBeSelected || sp.id === this._currentValue
    );

    return {
      key: 'id',
      label: 'displayName',
      items: _termOptions,
    };
  }
}
