export interface IPaginated<T> {
  pageIndex: number;
  totalPages: number;
  totalCount: number;
  items: T[];

  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
