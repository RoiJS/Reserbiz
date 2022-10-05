import { IDurationValueProvider } from "./iduration-value-provider.interface";
import { IValueTypeValueProvider } from "./ivalue-type-value-provider.interface";
import { ISpaceTypeValueProvider } from "./ispace-type-value-provider.interface";
import { IYesNoValueProvider } from "./iyesno-value-provider.interface";

export interface ITermFormValueProvider
  extends IDurationValueProvider,
    IValueTypeValueProvider,
    ISpaceTypeValueProvider,
    IYesNoValueProvider {}
