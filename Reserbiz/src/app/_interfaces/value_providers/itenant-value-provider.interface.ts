import { TenantOption } from '../../_models/options/tenant-option.model';

export interface ITenantValueProvider {
  tenantOptions: { key: string; label: string; items: TenantOption[] };
}
