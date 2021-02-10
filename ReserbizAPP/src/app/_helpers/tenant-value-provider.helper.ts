import { TranslateService } from '@ngx-translate/core';
import { take } from 'rxjs/operators';

import { TenantOption } from '../_models/tenant-option.model';
import { ITenantValueProvider } from '../_interfaces/itenant-value-provider.interface';
import { TenantService } from '../_services/tenant.service';

export class TenantValueProvider implements ITenantValueProvider {
  private _tenantsOptions: TenantOption[] = [];
  private _currentValue = 0;

  constructor(
    private translateService: TranslateService,
    private tenantService: TenantService
  ) {
    this.tenantService
      .getTenantAsOptions(this.translateService)
      .pipe(take(1))
      .subscribe((tenantOptions: TenantOption[]) => {
        // Define a default option
        const defaultTenantOption = new TenantOption();
        defaultTenantOption.id = 0;
        defaultTenantOption.name = '';
        defaultTenantOption.isActive = true;
        defaultTenantOption.isDelete = false;
        defaultTenantOption.canBeSelected = true;

        this._tenantsOptions.push(defaultTenantOption);
        this._tenantsOptions.push(...tenantOptions);
      });
  }

  getItem(itemId: number): TenantOption {
    return this.tenantOptions.items.find((s: TenantOption) => s.id === itemId);
  }

  setCurrenValue(value) {
    this._currentValue = value;
  }

  get tenantOptions(): {
    key: string;
    label: string;
    items: TenantOption[];
  } {
    // Filter options that are not inactive or the current value
    const _tenantOptions = this._tenantsOptions.filter(
      (sp) => sp.canBeSelected || sp.id === this._currentValue
    );

    return {
      key: 'id',
      label: 'displayName',
      items: _tenantOptions,
    };
  }
}
