import {Lazy} from '../data/lazy';
import uniqid from 'uniqid';
import {LazyUser} from '../data/lazy/lazy-user';
import {IUser} from './user';
import {Printer} from './printer';
import {LazyPrinter} from '../data/lazy/lazy-printer';
import {IEntity} from './ientity';

export class UserPrinter implements IEntity {
  get user(): Lazy<IUser> {
    return this._user;
  }

  get userId(): string {
    return this._userId;
  }

  set userId(value: string) {
    if (this._userId !== value)
      this._user = new LazyUser(value);

    this._userId = value;
  }

  get printer(): Lazy<Printer> {
    return this._printer;
  }

  get printerId(): string {
    return this._printerId;
  }

  set printerId(value: string) {
    if (this.printerId !== value)
      this._printer = new LazyPrinter(value);

    this._printerId = value;
  }

  id: string;

  private _printerId: string;
  private _printer: Lazy<Printer> = new LazyPrinter();
  private _userId: string;
  private _user: Lazy<IUser> = new LazyUser();
  isSaved: boolean;

  constructor() {
    this.id = uniqid();
  }

}
