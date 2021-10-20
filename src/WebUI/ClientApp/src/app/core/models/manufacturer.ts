export interface IManufacturer {
  id: string;
  name: string;
  verified: boolean;
}

export class Manufacturer implements IManufacturer{
  id: string;
  name: string;
  verified: boolean;
}
