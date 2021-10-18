import {Injectable} from '@angular/core';
import Dexie from 'dexie';
import {IManufacturer} from '../models/manufacturer';
import {UserPrinter} from '../models/user-printer';
import {IUser} from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AppDatabaseService extends Dexie {
  manufacturers: Dexie.Table<IManufacturer, string>;
  userPrinters: Dexie.Table<UserPrinter, string>;
  users: Dexie.Table<IUser, string>;

  constructor() {
    super('SeshatAppDatabase');

    this.version(1).stores({
      manufacturers: '++id, name, verified',
      userPrinters: '++id, userId',
      users: '++id, displayName'
    });

    this.manufacturers = this.table('manufacturers');
    this.userPrinters.mapToClass(UserPrinter);
    this.users = this.table('users');
  }
}



