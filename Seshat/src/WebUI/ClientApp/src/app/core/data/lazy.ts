import {AppDatabaseService} from './app-database.service';

export interface Lazy<T> {
  loaded: boolean;
  load(db: AppDatabaseService): Promise<any>;
  readonly value: T
}
