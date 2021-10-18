import {Lazy} from '../data/lazy';
import uniqid from 'uniqid';
import {LazyUser} from '../data/lazy/lazy-user';
import {IUser} from './user';

export class UserPrinter {
  get user(): Lazy<IUser> {
    return this._user;
  }

  get userId(): string {
    return this._userId;
  }

  set userId(value: string) {
    if (this._userId != value)
      this._user = new LazyUser(value);

    this._userId = value;
  }

  id: string;

  private _userId: string;
  private _user: Lazy<IUser> = new LazyUser();

  constructor() {
    this.id = uniqid();
  }
}
