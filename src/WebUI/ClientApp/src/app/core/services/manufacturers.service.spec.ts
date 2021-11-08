import {TestBed} from '@angular/core/testing';

import {ManufacturersService} from './manufacturers.service';
import {AppDatabaseService} from '../data/app-database.service';
import {ManufacturerInputModel} from '../models/manufacturer-input-model';
import {map, switchMap} from 'rxjs/operators';

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
    const input: ManufacturerInputModel = { name: 'Test Manufacturer' };
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
      expect(list.items.length).toBeGreaterThan(0);
      expect(list.pageIndex).toBe(1);
      done();
    });
  });

  it('should update manufacturer', (done) => {
    const input1: ManufacturerInputModel = { name: 'Test Manufacturer' };
    const input2: ManufacturerInputModel = { name: 'Test Manufacturer 2'};
    service.create(input1)
      .pipe(switchMap(man => service.update(man.id, input2 )))
      .subscribe(manufacturer => {
        expect(manufacturer).toBeTruthy();
        expect(manufacturer.name).toBeTruthy();
        expect(manufacturer.name).toBe(input2.name);
        expect(manufacturer.id).toBeTruthy();
        done();
      });
  });

  it('should delete manufacturer', (done) => {
    const input: ManufacturerInputModel = { name: 'Test Manufacturer' };
    service.create(input).pipe(
      switchMap((man) => service.delete(man.id).pipe(map(_ => man))),
      switchMap(man => service.get(man.id)))
      .subscribe(manufacturer => {
        expect(manufacturer).toBeFalsy();
        done();
      });
  });
});
