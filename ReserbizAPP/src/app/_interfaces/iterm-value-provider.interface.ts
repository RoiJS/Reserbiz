import { TermOption } from '../_models/term-option.model';

export interface ITermValueProvider {
  termOptions: { key: string; label: string; items: TermOption[] };
}
