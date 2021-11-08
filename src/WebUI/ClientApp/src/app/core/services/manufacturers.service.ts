import {Injectable} from '@angular/core';
import {AppDatabaseService} from '../data/app-database.service';
import {defer, Observable} from 'rxjs';
import {Manufacturer} from '../models/manufacturer';
import {ManufacturerInputModel} from '../models/manufacturer-input-model';
import {IPaginated} from '../models/ipaginated';
import {Paginated} from '../models/paginated';

@Injectable({
  providedIn: 'root'
})
export class ManufacturersService {

  constructor(private _db: AppDatabaseService) { }

  getList(name: string = '', pageIndex: number = 1, pageSize: number = 25): Observable<IPaginated<Manufacturer>> {
    return defer(async () => {
      const count = await this._db.manufacturers.count();
      const items = await this._db.manufacturers
        .filter(o => o.name.toLocaleLowerCase().includes(name.toLocaleLowerCase()))
        .offset((pageIndex - 1) * pageSize)
        .limit(pageSize)
        .toArray();

      return new Paginated<Manufacturer>(items, pageIndex, count, pageSize);
    });
  }

  get(id: string): Observable<Manufacturer> {
    return defer(() => this._db.manufacturers.get(id));
  }

  create(model: ManufacturerInputModel): Observable<Manufacturer> {
    return defer(async() => {
      const id = await this._db.manufacturers.add(new Manufacturer(model.name));
      return this._db.manufacturers.get(id);
    });
  }

  update(id: string, model: ManufacturerInputModel): Observable<Manufacturer> {
    return defer(async () => {
      const manufacturer = await this._db.manufacturers.get(id);

      manufacturer.name = model.name;
      await this._db.manufacturers.put(manufacturer);

      return this._db.manufacturers.get(id);
    });
  }

  delete(id: string): Observable<any> {
    return defer(() => this._db.manufacturers.delete(id));
  }
}
