import uniqid from 'uniqid';

export interface IUser {
  id: number;
  displayName: string;
}

export class User implements IUser {
  displayName: string = 'Maker';
  id: number;

  constructor() {
    this.id = uniqid();
  }
}
