import {IPaginated} from './ipaginated';

export class Paginated<T> implements IPaginated<T> {
  get hasNextPage(): boolean {
    return this.pageIndex < this.totalPages;
  }
  get hasPreviousPage(): boolean {
    return this.pageIndex > 1;
  }

  private readonly _totalPages: number;
  get totalPages(): number {
    return this._totalPages;
  }

  constructor(public readonly items: T[],
              public readonly pageIndex: number,
              public readonly totalCount: number,
              pageSize: number) {
    this._totalPages = totalCount / pageSize;
  }

}
