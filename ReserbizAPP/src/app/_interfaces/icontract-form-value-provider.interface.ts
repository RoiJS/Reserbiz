import { ITenantValueProvider } from './itenant-value-provider.interface';
import { IDurationValueProvider } from './iduration-value-provider.interface';
import { ITermValueProvider } from './iterm-value-provider.interface';

export interface IContractFormValueProvider
  extends ITermValueProvider,
    ITenantValueProvider,
    IDurationValueProvider {}
