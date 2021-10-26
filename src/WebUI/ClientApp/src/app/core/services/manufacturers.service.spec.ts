import {TestBed} from '@angular/core/testing';

import {ManufacturersService} from './manufacturers.service';
import {AppDatabaseService} from '../data/app-database.service';
import {ManufacturerInputModel} from '../models/manufacturer-input-model';

describe('ManufacturersService', () => {
  let service: ManufacturersService;
  let database: AppDatabaseService;
  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ManufacturersService);
    database = TestBed.inject(AppDatabaseService);
  });

  afterEach(async () => {
    await database.delete();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should add manufacturer', (done) => {
    const input: ManufacturerInputModel = { name: 'Test Manufacturer' }
    service.create(input)
      .subscribe(manufacturer => {
        expect(manufacturer).toBeTruthy();
        expect(manufacturer.name).toBeTruthy();
        expect(manufacturer.name).toBe(input.name);
        expect(manufacturer.id).toBeTruthy();
        done();
      });
  });

  it('should get list', (done) => {
    service.getList().subscribe(list => {
      expect(list).toBeTruthy();
      expect(list.length).toBeGreaterThan(0);
      done();
    })
  });
});
