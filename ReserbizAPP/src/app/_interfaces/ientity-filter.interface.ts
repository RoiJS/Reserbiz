export interface IEntityFilter {
  parentId?: number;
  page?: number;
  searchKeyword?: string;

  reset(): void;
  isFilterActive(): boolean;
}
