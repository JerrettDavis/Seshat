import {IManufacturer} from './manufacturer';
import {Lazy} from '../data/lazy';
import uniqid from 'uniqid';
import {LazyManufacturer} from '../data/lazy/lazy-manufacturer';
import {IEntity} from './ientity';

export class Printer implements IEntity {
  get manufacturer(): Lazy<IManufacturer> {
    return this._manufacturer;
  }
  get manufacturerId(): string {
    return this._manufacturerId;
  }

  set manufacturerId(value: string) {
    if (this._manufacturerId != value)
      this._manufacturer = new LazyManufacturer(value);

    this._manufacturerId = value;
  }

  id: string;
  private _manufacturerId: string;
  private _manufacturer: Lazy<IManufacturer> = new LazyManufacturer();
  model: string;
  verified: boolean;
  isSaved: boolean;

  constructor() {
    this.id = uniqid();
  }

  makeReady() {
    this._manufacturer = new LazyManufacturer(this.id);
    return this;
  }


}
