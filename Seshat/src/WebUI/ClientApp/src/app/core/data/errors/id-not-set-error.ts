export class IdNotSetError extends Error {
  constructor(message: string | null = null) {
    super(message);

    Object.setPrototypeOf(this, IdNotSetError.prototype);
  }
}
