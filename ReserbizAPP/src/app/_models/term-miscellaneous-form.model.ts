import { BaseForm } from './base-form.model';

export class TermMiscellaneousFormSource extends BaseForm<
  TermMiscellaneousFormSource
> {
  constructor(
    public name: string,
    public description: string,
    public amount: number
  ) {
    super();
  }

  clone(): TermMiscellaneousFormSource {
    return new TermMiscellaneousFormSource(
      this.name,
      this.description,
      this.amount
    );
  }
}
