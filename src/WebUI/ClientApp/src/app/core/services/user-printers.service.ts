import {Injectable} from '@angular/core';
import {AppDatabaseService} from '../data/app-database.service';
import {defer, Observable} from 'rxjs';
import {UserPrinter} from '../models/user-printer';

@Injectable({
  providedIn: 'root'
})
export class UserPrintersService {

  constructor(private _db: AppDatabaseService) { }

  getList(): Observable<UserPrinter[]> {
    return defer(() => this._db.userPrinters.toArray());
  }

  get(id: string): Observable<UserPrinter> {
    return defer(() => this._db.userPrinters.get(id));
  }
}
