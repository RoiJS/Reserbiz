import { Injectable } from '@angular/core';

import { TermMiscellaneous } from '../_models/term-miscellaneous.model';
import { EntityListService } from './entity-list.service';

@Injectable({ providedIn: 'root' })
export class AddTermMiscellaneousService extends EntityListService<
  TermMiscellaneous
> {
  constructor() {
    super();
  }

  isSame(otherTermMiscellaneousList: TermMiscellaneous[]): boolean {
    // Check the length between the other term miscellaneous
    // and the current term miscellaneous list
    if (otherTermMiscellaneousList.length !== this.entityList.value.length) {
      return false;
    }

    // Check each item on other term miscellaneous object against
    // each item on the current term miscellaneous list
    for (
      let currentIndex = 0;
      currentIndex < otherTermMiscellaneousList.length;
      currentIndex++
    ) {
      const termMiscellaneous = otherTermMiscellaneousList[currentIndex];
      const currentTermMiscellaneousFromList = this.entityList.value[
        currentIndex
      ];
      const hasChanged = Boolean(
        termMiscellaneous.name !== currentTermMiscellaneousFromList.name ||
          termMiscellaneous.description !==
            currentTermMiscellaneousFromList.description ||
          termMiscellaneous.amount !== currentTermMiscellaneousFromList.amount
      );

      // Check if current item is no longer equal
      // with its corresponding item from the current term
      // miscellaneous list.
      if (hasChanged) {
        return false;
      }
    }

    return true;
  }
}
