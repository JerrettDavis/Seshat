import {IEntity} from './ientity';
import uniqid from 'uniqid';

export interface IManufacturer {
  id: string;
  name: string;
  verified: boolean;
}

export class Manufacturer implements IManufacturer, IEntity{
  id: string;
  verified: boolean;
  lastSynced?: Date;

  constructor(public name: string) {
    this.id = uniqid();
  }
}
