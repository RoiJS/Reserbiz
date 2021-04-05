import { SortOrderEnum } from '../../_enum/sort-order.enum';

export interface ISortOrderValueProvider {
  sortOrderOptions: Array<{ key: SortOrderEnum; label: string }>;
}
