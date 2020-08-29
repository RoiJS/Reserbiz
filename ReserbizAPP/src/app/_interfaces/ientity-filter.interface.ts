export interface IEntityFilter {
  parentId?: number;
  page?: number;
  searchKeyword?: string;

  reset(): void;
  toFilterJSON(): any;
  isFilterActive(): boolean;
}
