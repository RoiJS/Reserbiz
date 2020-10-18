import { BaseForm } from './base-form.model';
import { IBaseFormSource } from '../_interfaces/ibase-form-source.interface';

export class SpaceTypeFormSource extends BaseForm<SpaceTypeFormSource>
  implements IBaseFormSource<SpaceTypeFormSource> {
  constructor(
    public name: string,
    public description: string,
    public rate: number
  ) {
    super();
  }

  clone() {
    return new SpaceTypeFormSource(
      this.name,
      this.description,
      this.rate
    );
  }
}
