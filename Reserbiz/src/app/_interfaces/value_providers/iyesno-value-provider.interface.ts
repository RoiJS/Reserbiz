import { YesNoEnum } from '~/app/_enum/yesno-unit.enum';

export interface IYesNoValueProvider {
  yesNoOptions: Array<{ key: YesNoEnum; label: string }>;
}
