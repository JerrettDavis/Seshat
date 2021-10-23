import { Injectable } from '@angular/core';
import {Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConnectionStateService {
  private _connectionChanged = new Subject<boolean>()

  get connectionChanged(): Observable<boolean> {
    return this._connectionChanged.asObservable();
  }

  get isOnline(): boolean {
    return !!window.navigator.onLine;
  }

  constructor() {
    window.addEventListener('online', () => this.updateOnlineStatus());
    window.addEventListener('offline', () => this.updateOnlineStatus());
  }

  private updateOnlineStatus(): void {
    this._connectionChanged.next(this.isOnline);
  }
}
