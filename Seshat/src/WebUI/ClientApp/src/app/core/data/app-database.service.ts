import {Injectable} from '@angular/core';
import Dexie from 'dexie';
import {Manufacturer} from '../models/manufacturer';
import {UserPrinter} from '../models/user-printer';
import {IUser, User} from '../models/user';
import {Printer} from '../models/printer';
import * as manufacturers from './seeds/manufacturers.json';
import * as printers from './seeds/printers.json';

@Injectable({
  providedIn: 'root'
})
export class AppDatabaseService extends Dexie {
  manufacturers: Dexie.Table<Manufacturer, string>;
  printers: Dexie.Table<Printer, string>;
  userPrinters: Dexie.Table<UserPrinter, string>;
  users: Dexie.Table<IUser, string>;

  constructor() {
    super('SeshatAppDatabase');

    this.version(1).stores({
      manufacturers: '++id, name, verified',
      printers: '++id, manufacturerId, model, verified',
      userPrinters: '++id, userId',
      users: '++id, displayName'
    });

    this.on('populate', () => {
      this.manufacturers.bulkAdd(manufacturers.manufacturers);
      this.printers.bulkAdd(printers.printers.map(p => {
        const printer = new Printer();

        printer.id = p.id;
        printer.manufacturerId = p.manufacturerId;
        printer.model = p.model;
        printer.verified = p.verified;

        return printer;
      }));
      this.users.add(new User())
    });

    this.manufacturers = this.table('manufacturers');
    this.printers.mapToClass(Printer);
    this.userPrinters.mapToClass(UserPrinter);
    this.users = this.table('users');
  }
}



