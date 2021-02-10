import { CalendarEvent } from 'nativescript-ui-calendar';
import { Contract } from './contract.model';

import { Color } from '@nativescript/core';

export class ContractCalendarEvent extends CalendarEvent {
  constructor(public contract: Contract) {
    super(
      `${contract.code} - ${contract.tenantName}`,
      contract.nextDueDate,
      contract.nextDueDate,
      true,
      new Color('#eb5a2e')
    );
  }
}
