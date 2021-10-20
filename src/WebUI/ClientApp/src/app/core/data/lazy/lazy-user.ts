import {Lazy} from '../lazy';
import {IUser} from '../../models/user';
import {AppDatabaseService} from '../app-database.service';
import {IdNotSetError} from '../errors/id-not-set-error';
import {NavigationNotLoadedError} from '../errors/navigation-not-loaded-error';

export class LazyUser implements Lazy<IUser> {
  loaded: boolean;

  private readonly _id: string | null = null;
  private _value: IUser | null = null;

  constructor(id: string | null = null) {
    this._id = id;
  }

  async load(db: AppDatabaseService): Promise<any> {
    if (!this._id)
      throw new IdNotSetError();

    this._value = await db.users.get(this._id);
  }

  get value(): IUser {
    if (!this._value)
      throw new NavigationNotLoadedError()

    return this._value;
  }
}
