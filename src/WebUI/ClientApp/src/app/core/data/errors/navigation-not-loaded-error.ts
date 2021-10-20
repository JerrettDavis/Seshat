export class NavigationNotLoadedError extends Error {
  constructor(message: string | null = null) {
    super(message);

    Object.setPrototypeOf(this, NavigationNotLoadedError.prototype);
  }
}
