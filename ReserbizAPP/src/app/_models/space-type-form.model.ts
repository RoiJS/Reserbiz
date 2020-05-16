import { BaseForm } from './base-form.model';

export class SpaceTypeFormSource extends BaseForm<SpaceTypeFormSource> {
  constructor(
    public name: string,
    public description: string,
    public rate: number,
    public availableSlot: number
  ) {
    super();
  }

  clone() {
    return new SpaceTypeFormSource(
      this.name,
      this.description,
      this.rate,
      this.availableSlot
    );
  }
}
