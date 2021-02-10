import { TenantOption } from '../_models/tenant-option.model';

export interface ITenantValueProvider {
  tenantOptions: { key: string; label: string; items: TenantOption[] };
}
