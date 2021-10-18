export interface IUser {
  id: number;
  displayName: string;
}

export class User implements IUser{
  displayName: string;
  id: number;
}
