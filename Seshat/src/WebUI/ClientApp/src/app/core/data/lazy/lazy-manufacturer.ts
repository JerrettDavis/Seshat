import {IManufacturer} from '../../models/manufacturer';
import {Lazy} from '../lazy';
import {AppDatabaseService} from '../app-database.service';
import {IdNotSetError} from '../errors/id-not-set-error';
import {NavigationNotLoadedError} from '../errors/navigation-not-loaded-error';

export class LazyManufacturer implements Lazy<IManufacturer> {
  loaded: boolean;
  private readonly _id: string | null = null;

  private _value: IManufacturer | null = null;

  constructor(id: string | null = null) {
    this._id = id;
  }

  async load(db: AppDatabaseService): Promise<any> {
    if (!this._id)
      throw new IdNotSetError();

    this._value = await db.manufacturers.get(this._id);
  }

  get value(): IManufacturer {
    if (!this._value)
      throw new NavigationNotLoadedError();

    return this._value;
  }

}
