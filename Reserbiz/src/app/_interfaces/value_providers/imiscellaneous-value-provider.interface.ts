import { MiscellaneousDueDateEnum } from '../../_enum/miscellaneous-due-date.enum';

export interface IMiscellaneousValueProvider {
  dueDateOptions: Array<{ key: MiscellaneousDueDateEnum; label: string }>;
}
