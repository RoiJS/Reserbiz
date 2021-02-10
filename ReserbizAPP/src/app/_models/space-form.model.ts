import { IBaseFormSource } from '../_interfaces/ibase-form-source.interface';
import { BaseForm } from './base-form.model';

export class SpaceFormSource
  extends BaseForm<SpaceFormSource>
  implements IBaseFormSource<SpaceFormSource> {
  constructor(public description: string, public spaceTypeId: number) {
    super();
  }

  clone() {
    return new SpaceFormSource(this.description, this.spaceTypeId);
  }
}
