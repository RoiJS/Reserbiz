import { SortOrderEnum } from '../../_enum/sort-order.enum';

export interface IEntityFilter {
  parentId?: number;
  page?: number;
  searchKeyword?: string;
  sortOrder?: SortOrderEnum;

  reset(): void;
  toFilterJSON(): any;
  isFilterActive(): boolean;
}
