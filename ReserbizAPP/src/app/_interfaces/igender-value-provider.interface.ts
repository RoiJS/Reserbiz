import { GenderEnum } from '../_enum/gender.enum';

export interface IGenderValueProvider {
  genderOptions: Array<{ key: GenderEnum; label: string }>;
}
