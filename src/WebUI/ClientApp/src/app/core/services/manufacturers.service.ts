import {Injectable} from '@angular/core';
import {AppDatabaseService} from '../data/app-database.service';
import {defer, Observable} from 'rxjs';
import {Manufacturer} from '../models/manufacturer';
import {ManufacturerInputModel} from '../models/manufacturer-input-model';

@Injectable({
  providedIn: 'root'
})
export class ManufacturersService {

  constructor(private _db: AppDatabaseService) { }

  getList(): Observable<Manufacturer[]> {
    return defer(() => this._db.manufacturers.toArray());
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
    })
  }

  delete(id: string): Observable<any> {
    return defer(() => this._db.manufacturers.delete(id));
  }
}
