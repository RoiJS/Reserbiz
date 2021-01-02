import { IBaseEntityMapper } from '../_interfaces/ibase-entity-mapper.interface';

import { Penalty } from '../_models/penalty.model';

export class PenaltyMapper implements IBaseEntityMapper<Penalty> {
  mapEntity(p: Penalty): Penalty {
    const penalty = new Penalty();
    penalty.id = p.id;
    penalty.dueDate = p.dueDate;
    penalty.amount = p.amount;
    return penalty;
  }
}
