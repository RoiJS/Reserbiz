import { TermOption } from '../../_models/options/term-option.model';

export interface ITermValueProvider {
  termOptions: { key: string; label: string; items: TermOption[] };
}
