import { TestBed } from '@angular/core/testing';

import { UserPrintersService } from './user-printers.service';

describe('UserPrintersService', () => {
  let service: UserPrintersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserPrintersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
