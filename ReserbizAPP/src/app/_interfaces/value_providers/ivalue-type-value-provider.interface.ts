import { ValueTypeEnum } from '../../_enum/value-type.enum';

export interface IValueTypeValueProvider {
  valueTypeOptions: Array<{ key: ValueTypeEnum; label: string }>;
}
