import {IEntity} from './ientity';

export interface IManufacturer {
  id: string;
  name: string;
  verified: boolean;
}

export class Manufacturer implements IManufacturer, IEntity{
  id: string;
  name: string;
  verified: boolean;
  isSaved: boolean;
}
